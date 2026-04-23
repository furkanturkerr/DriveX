using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.MessageDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class MessageManager : IMessageService
{
    private readonly IMessageDal _messageDal;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public MessageManager(IMessageDal messageDal, IMapper mapper, IConfiguration configuration)
    {
        _messageDal = messageDal;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<List<ResultMessageDto>> TGetListAsync()
    {
        var values = await _messageDal.GetListAsync();
        return _mapper.Map<List<ResultMessageDto>>(values);
    }

    public async Task<UpdateMessageDto> TGetByIdAsync(int id)
    {
        var values = await _messageDal.GetByIdAsync(id);
        values.IsRead = true;
        await _messageDal.UpdateAsync(values);
        return _mapper.Map<UpdateMessageDto>(values);
    }

    public async Task TInsertAsync(CreateMessageDto dto)
    {
        var entity = _mapper.Map<Message>(dto);

        string aiReply;

        try
        {
            aiReply = await GenerateAiReplyAsync(dto);
        }
        catch
        {
            aiReply = GetFallbackReply(dto.NameSurname);
        }

        entity.MessageAI = aiReply;

        await _messageDal.InsertAsync(entity);

        try
        {
            await SendContactReplyEmail(
                email: dto.Email,
                namesurname: dto.NameSurname,
                phone: dto.Phone,
                message: dto.Messages,
                messageai: aiReply
            );
        }
        catch
        {
            // Mail gönderimi patlasa bile mesaj kaydı veritabanına düşmüş olsun.
        }
    }

    public async Task TUpdateAsync(UpdateMessageDto dto)
    {
        var values = _mapper.Map<Message>(dto);
        await _messageDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _messageDal.DeleteAsync(id);
    }

    private async Task<string> GenerateAiReplyAsync(CreateMessageDto dto)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("OpenAI API key bulunamadı.");

        if (string.IsNullOrWhiteSpace(model))
            model = "gpt-4o-mini";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var systemPrompt = """
Bir araç kiralama markasının müşteri destek asistanısın.
Marka adı: DriveX.

Görevin:
- Kullanıcının iletişim formuna Türkçe, profesyonel, sıcak ve net bir cevap üretmek.
- Cevap en fazla 120-160 kelime olsun.
- Gereksiz uzun yazma.
- Kullanıcıyı adıyla hitap et.
- Mesajın içeriğine göre doğal cevap ver.
- Fiyat, rezervasyon, araç uygunluğu, ödeme, iade, ehliyet, teslim noktası gibi konularda yardımcı ol.
- Emin olmadığın bilgi varsa uydurma; “ekibimiz sizinle en kısa sürede iletişime geçecektir” tarzı güvenli ifade kullan.
- HTML üretme, sadece düz metin üret.
- Madde madde yazma, doğal bir e-posta cevabı gibi yaz.
- Cevabın sonunda kısa bir kapanış olsun:
  “DriveX Ekibi” ile bitir.
""";

        var userPrompt = $"""
Müşteri bilgileri:
Ad Soyad: {dto.NameSurname}
Telefon: {dto.Phone}
E-posta: {dto.Email}

Müşterinin mesajı:
{dto.Messages}

Buna uygun bir müşteri yanıtı üret.
""";

        var requestBody = new
        {
            model = model,
            messages = new object[]
            {
                new
                {
                    role = "system",
                    content = systemPrompt
                },
                new
                {
                    role = "user",
                    content = userPrompt
                }
            },
            temperature = 0.7,
            max_tokens = 220
        };

        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            throw new Exception($"OpenAI hatası: {response.StatusCode} - {errorText}");
        }

        var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

        var content = result?.Choices?
            .FirstOrDefault()?
            .Message?
            .Content?
            .Trim();

        if (string.IsNullOrWhiteSpace(content))
            throw new Exception("OpenAI boş cevap döndürdü.");

        return content;
    }

    private string GetFallbackReply(string? namesurname)
    {
        var customer = string.IsNullOrWhiteSpace(namesurname) ? "Değerli müşterimiz" : namesurname;

        return $"""
Merhaba {customer},

Mesajınız tarafımıza ulaştı. İlginiz için teşekkür ederiz.
Ekibimiz talebinizi inceleyip size en kısa sürede dönüş sağlayacaktır.

Acil bir durum varsa iletişim bilgilerinizi kontrol ederek yeniden bizimle paylaşabilirsiniz.

İyi günler dileriz.
DriveX Ekibi
""";
    }

    private async Task SendContactReplyEmail(
        string email,
        string namesurname,
        string phone,
        string message,
        string messageai)
    {
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderPassword = _configuration["EmailSettings:SmtpPassword"];

        if (string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(senderPassword))
            throw new Exception("Mail ayarları eksik.");

        MimeMessage mimeMessage = new();
        mimeMessage.From.Add(new MailboxAddress("DriveX", senderEmail));
        mimeMessage.To.Add(new MailboxAddress(namesurname, email));
        mimeMessage.Subject = "📩 Mesajınıza Cevabımız — DriveX";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = GetContactReplyMailTemplate(namesurname, phone, message, messageai)
        };

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync(senderEmail, senderPassword);
        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }

    private string GetContactReplyMailTemplate(string namesurname, string phone, string message, string messageai)
    {
        return $@"<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>DriveX İletişim Yanıtı</title>
</head>
<body style='margin:0;padding:0;background:#f3f4f6;font-family:Arial,Helvetica,sans-serif;color:#111827;'>

<table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0' style='background:#f3f4f6;'>
    <tr>
        <td align='center' style='padding:24px 12px;'>

            <table role='presentation' width='640' cellpadding='0' cellspacing='0' border='0'
                   style='width:640px;max-width:640px;background:#ffffff;border-radius:20px;overflow:hidden;'>

                <tr>
                    <td style='background:#0f172a;padding:22px 30px;'>
                        <table role='presentation' cellpadding='0' cellspacing='0' border='0'>
                            <tr>
                                <td style='width:50px;height:50px;background:#22c55e;border-radius:14px;text-align:center;vertical-align:middle;'>
                                    <span style='font-size:24px;line-height:50px;color:#ffffff;font-weight:bold;'>✉</span>
                                </td>
                                <td style='padding-left:14px;vertical-align:middle;'>
                                    <div style='font-size:22px;line-height:22px;font-weight:800;color:#ffffff;'>
                                        Drive<span style='color:#22c55e;'>X</span>
                                    </div>
                                    <div style='font-size:12px;line-height:18px;color:#94a3b8;padding-top:6px;'>
                                        Premium Car Rental
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td style='background:linear-gradient(135deg,#0f172a 0%,#111827 100%);padding:40px 30px;text-align:center;'>
                        <div style='font-size:34px;line-height:42px;font-weight:800;color:#ffffff;'>
                            Mesajınıza Dönüş Yaptık
                        </div>
                        <div style='font-size:16px;line-height:28px;color:#d1d5db;padding-top:14px;'>
                            Merhaba <span style='color:#22c55e;font-weight:700;'>{EscapeHtml(namesurname)}</span>,<br>
                            iletişim talebinize ilişkin yanıtımız aşağıdadır.
                        </div>
                    </td>
                </tr>

                <tr>
                    <td style='padding:32px;'>

                        <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                               style='background:#f9fafb;border:1px solid #e5e7eb;border-radius:16px;'>
                            <tr>
                                <td style='padding:22px;'>
                                    <div style='font-size:12px;font-weight:800;letter-spacing:1px;text-transform:uppercase;color:#22c55e;padding-bottom:14px;border-bottom:1px solid #d1fae5;'>
                                        Gönderdiğiniz Bilgiler
                                    </div>

                                    <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0' style='padding-top:8px;'>
                                        <tr>
                                            <td style='padding:12px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;width:34%;'>Ad Soyad</td>
                                            <td style='padding:12px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{EscapeHtml(namesurname)}</td>
                                        </tr>
                                        <tr>
                                            <td style='padding:12px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>Telefon</td>
                                            <td style='padding:12px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{EscapeHtml(phone)}</td>
                                        </tr>
                                        <tr>
                                            <td style='padding:12px 0 4px 0;font-size:14px;color:#6b7280;vertical-align:top;'>Mesajınız</td>
                                            <td style='padding:12px 0 4px 0;font-size:14px;color:#111827;font-weight:700;line-height:24px;'>{Nl2Br(EscapeHtml(message))}</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                        <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                               style='margin-top:24px;background:#0f172a;border-radius:18px;overflow:hidden;'>
                            <tr>
                                <td style='padding:16px 22px;background:#111827;color:#cbd5e1;font-size:12px;font-weight:800;letter-spacing:1px;text-transform:uppercase;'>
                                    DriveX Yanıtı
                                </td>
                            </tr>
                            <tr>
                                <td style='padding:24px 22px;font-size:15px;line-height:28px;color:#e5e7eb;'>
                                    {Nl2Br(EscapeHtml(messageai))}
                                </td>
                            </tr>
                        </table>

                        <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                               style='margin-top:24px;background:#f9fafb;border:1px solid #e5e7eb;border-radius:16px;'>
                            <tr>
                                <td style='padding:22px;'>
                                    <div style='font-size:12px;font-weight:800;letter-spacing:1px;text-transform:uppercase;color:#6b7280;padding-bottom:14px;'>
                                        Not
                                    </div>
                                    <div style='font-size:14px;line-height:26px;color:#374151;'>
                                        Bu e-posta otomatik olarak oluşturulmuştur. Gerekli durumlarda ekibimiz sizinle ayrıca iletişime geçebilir.
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>

                <tr>
                    <td style='background:#0f172a;padding:26px 30px;text-align:center;'>
                        <div style='font-size:22px;line-height:22px;font-weight:800;color:#ffffff;padding-bottom:10px;'>
                            Drive<span style='color:#22c55e;'>X</span>
                        </div>
                        <div style='font-size:12px;line-height:22px;color:#94a3b8;'>
                            Premium Car Rental<br>
                            Bu e-posta DriveX iletişim sistemi tarafından gönderilmiştir.
                        </div>
                    </td>
                </tr>

            </table>

        </td>
    </tr>
</table>

</body>
</html>";
    }

    private static string EscapeHtml(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }

    private static string Nl2Br(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value.Replace("\r\n", "<br>").Replace("\n", "<br>");
    }

    public class OpenAIResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice>? Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public ChatMessage? Message { get; set; }
    }

    public class ChatMessage
    {
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}