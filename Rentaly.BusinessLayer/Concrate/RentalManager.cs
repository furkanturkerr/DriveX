using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class RentalManager : IRentalService
{
    private readonly IRentalDal _rentalDal;
    private readonly ICarDal _carDal;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public RentalManager(
        IRentalDal rentalDal,
        ICarDal carDal,
        IMapper mapper,
        IConfiguration configuration)
    {
        _rentalDal = rentalDal;
        _carDal = carDal;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<List<ResultRentalDto>> TGetListAsync()
    {
        var values = await _rentalDal.GetListAsync();
        return _mapper.Map<List<ResultRentalDto>>(values);
    }

    public async Task<UpdateRentalDto> TGetByIdAsync(int id)
    {
        var value = await _rentalDal.GetByIdAsync(id);
        return _mapper.Map<UpdateRentalDto>(value);
    }

    public async Task TInsertAsync(CreateRentalDto dto)
    {
        var car = await _carDal.GetByIdAsync(dto.CarId);

        if (car == null)
            throw new Exception("Araç bulunamadı.");

        if (dto.PickupDate == default || dto.DropoffDate == default)
            throw new Exception("Tarih bilgisi eksik.");

        var totalDays = (dto.DropoffDate - dto.PickupDate).Days;

        if (totalDays <= 0)
            throw new Exception("Teslim tarihi alış tarihinden sonra olmalıdır.");

        var entity = _mapper.Map<Rental>(dto);

        entity.DailyPrice = car.DailyPrice;
        entity.TotalDays = totalDays;
        entity.TotalPrice = entity.DailyPrice * totalDays;
        entity.Status = RentalStatus.Beklemede;
        entity.CreatedAt = DateTime.Now;

        await _rentalDal.InsertAsync(entity);
    }
    
    public async Task<List<int>> TGetUnavailableCarIdsAsync(DateTime pickupDate, DateTime dropoffDate)
    {
      return await _rentalDal.GetUnavailableCarIdsAsync(pickupDate, dropoffDate);
    }

    public async Task<List<ResultRentalDto>> GetRentalWithDetailsAsync()
    {
      var values = await _rentalDal.GetRentalWithDetailsAsync();
      return _mapper.Map<List<ResultRentalDto>>(values);
    }
    
    public async Task<List<object>> TGetBookedDatesAsync(int carId)
    {
      var rentals = await _rentalDal.GetRentalsByCarIdAsync(carId);

      return rentals
        .Where(r =>
          r.Status != RentalStatus.Reddedildi &&
          r.Status != RentalStatus.Iptal &&
          r.Status != RentalStatus.Tamamlandi)
        .Select(r => (object)new
        {
          start = r.PickupDate.ToString("yyyy-MM-dd"),
          end   = r.DropoffDate.ToString("yyyy-MM-dd")
        })
        .ToList();
    }

    public async Task TUpdateAsync(UpdateRentalDto dto)
    {
        await _rentalDal.UpdateRentalStatusAndCarAsync(dto.RentalId, dto.Status, dto.AdminNote);

        if (dto.Status == (int)RentalStatus.Onaylandi)
        {
            var rental = await _rentalDal.GetByIdAsync(dto.RentalId);

            if (rental == null)
                throw new Exception($"Rezervasyon bulunamadı: {dto.RentalId}");

            var car = await _carDal.GetCarWithDetailsAsync(rental.CarId);

            if (car == null)
                throw new Exception($"Araç bulunamadı: {rental.CarId}");

            var brandName = car.Brand?.BrandName ?? "Marka";
            var modelName = car.CarModel?.ModelName ?? "";
            var carName = $"{brandName} {modelName} ({car.Year})";

            await SendReservationApprovalEmail(
                email: rental.Email,
                customerName: $"{rental.FirstName} {rental.LastName}",
                carName: carName,
                startDate: rental.PickupDate.ToString("dd MMMM yyyy HH:mm"),
                endDate: rental.DropoffDate.ToString("dd MMMM yyyy HH:mm"),
                totalDays: rental.TotalDays,
                dailyPrice: rental.DailyPrice,
                totalPrice: rental.TotalPrice,
                discountCode: GenerateDiscountCode()
            );
        }
    }

    public async Task TDeleteAsync(int id)
    {
        await _rentalDal.DeleteAsync(id);
    }

    private string GenerateDiscountCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        return $"DRX-{code}";
    }

    private async Task SendReservationApprovalEmail(
        string email,
        string customerName,
        string carName,
        string startDate,
        string endDate,
        int totalDays,
        decimal dailyPrice,
        decimal totalPrice,
        string discountCode)
    {
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderPassword = _configuration["EmailSettings:SmtpPassword"];

        MimeMessage mimeMessage = new();
        mimeMessage.From.Add(new MailboxAddress("DriveX", senderEmail));
        mimeMessage.To.Add(new MailboxAddress(customerName, email));
        mimeMessage.Subject = "✅ Rezervasyonunuz Onaylandı — DriveX";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = GetApprovalMailTemplate(
                customerName,
                carName,
                startDate,
                endDate,
                totalDays,
                dailyPrice,
                totalPrice,
                discountCode)
        };

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync(senderEmail, senderPassword);
        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }

    private string GetApprovalMailTemplate(
    string customerName,
    string carName,
    string startDate,
    string endDate,
    int totalDays,
    decimal dailyPrice,
    decimal totalPrice,
    string discountCode)
{
    return $@"<!DOCTYPE html>
<html lang='tr'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Rezervasyon Onayı</title>
</head>
<body style='margin:0;padding:0;background-color:#f3f4f6;font-family:Arial,Helvetica,sans-serif;color:#111827;'>

  <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#f3f4f6;margin:0;padding:0;'>
    <tr>
      <td align='center' style='padding:24px 12px;'>

        <table role='presentation' width='640' cellpadding='0' cellspacing='0' border='0' style='width:640px;max-width:640px;background-color:#ffffff;border-radius:20px;overflow:hidden;'>
          
          <!-- HEADER -->
          <tr>
            <td style='background-color:#0f172a;padding:24px 32px;'>
              <table role='presentation' cellpadding='0' cellspacing='0' border='0'>
                <tr>
                  <td style='width:52px;height:52px;background-color:#22c55e;border-radius:14px;text-align:center;vertical-align:middle;'>
                    <span style='font-size:24px;line-height:52px;color:#ffffff;font-weight:bold;'>✓</span>
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

          <!-- HERO -->
          <tr>
            <td style='background:linear-gradient(135deg,#0f172a 0%,#111827 100%);padding:42px 32px 38px 32px;text-align:center;'>
              <table role='presentation' align='center' cellpadding='0' cellspacing='0' border='0'>
                <tr>
                  <td style='width:84px;height:84px;background-color:#22c55e;border-radius:42px;text-align:center;vertical-align:middle;'>
                    <span style='font-size:40px;line-height:84px;color:#ffffff;font-weight:bold;'>✓</span>
                  </td>
                </tr>
              </table>

              <div style='font-size:38px;line-height:46px;font-weight:800;color:#ffffff;padding-top:22px;'>
                Rezervasyonunuz Onaylandı
              </div>

              <div style='font-size:16px;line-height:28px;color:#d1d5db;padding-top:16px;'>
                Sayın <span style='color:#22c55e;font-weight:700;'>{customerName}</span>, rezervasyonunuz başarıyla onaylandı.<br>
                Tüm detayları aşağıda bulabilirsiniz.
              </div>
            </td>
          </tr>

          <!-- CONTENT -->
          <tr>
            <td style='padding:34px 32px 12px 32px;'>

              <div style='font-size:15px;line-height:30px;color:#374151;padding-bottom:26px;'>
                Merhaba <strong style='color:#111827;'>{customerName}</strong>,<br><br>
                <strong style='color:#111827;'>{carName}</strong> için oluşturduğunuz rezervasyon onaylandı.
                Belirlediğiniz tarihte şubemize gelerek işlemlerinizi tamamlayabilirsiniz.
              </div>

              <!-- DETAIL CARD -->
              <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                     style='background-color:#f9fafb;border:1px solid #e5e7eb;border-radius:16px;'>
                <tr>
                  <td style='padding:24px 22px 18px 22px;'>

                    <div style='font-size:12px;line-height:12px;font-weight:800;letter-spacing:1px;text-transform:uppercase;color:#22c55e;padding-bottom:14px;border-bottom:1px solid #d1fae5;'>
                      Rezervasyon Detayları
                    </div>

                    <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0' style='padding-top:8px;'>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;width:42%;'>Ad Soyad</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{customerName}</td>
                      </tr>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>Araç</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{carName}</td>
                      </tr>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>Alış Tarihi</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{startDate}</td>
                      </tr>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>İade Tarihi</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{endDate}</td>
                      </tr>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>Kiralama Süresi</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>{totalDays} gün</td>
                      </tr>
                      <tr>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#6b7280;'>Günlük Fiyat</td>
                        <td style='padding:14px 0;border-bottom:1px solid #e5e7eb;font-size:14px;color:#111827;font-weight:700;'>₺{dailyPrice:N2}</td>
                      </tr>
                      <tr>
                        <td style='padding:16px 0 8px 0;font-size:14px;color:#6b7280;'>Toplam Tutar</td>
                        <td style='padding:16px 0 8px 0;font-size:20px;color:#22c55e;font-weight:800;'>₺{totalPrice:N2}</td>
                      </tr>
                    </table>

                  </td>
                </tr>
              </table>

              <!-- DISCOUNT -->
              <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                     style='margin-top:28px;border-radius:18px;overflow:hidden;'>
                <tr>
                  <td style='background-color:#111827;padding:14px 20px;font-size:12px;line-height:18px;font-weight:800;letter-spacing:1px;text-transform:uppercase;color:#cbd5e1;'>
                    Size Özel — Sonraki Rezervasyonunuz İçin
                  </td>
                </tr>
                <tr>
                  <td style='background:linear-gradient(135deg,#0f172a 0%,#111827 100%);padding:34px 24px;text-align:center;'>
                    <div style='font-size:20px;line-height:28px;font-weight:800;color:#ffffff;padding-bottom:8px;'>
                      Özel İndirim Kodunuz
                    </div>
                    <div style='font-size:15px;line-height:26px;color:#cbd5e1;padding-bottom:24px;'>
                      Bir sonraki araç kiralama işleminizde geçerli.<br>
                      Tek kullanımlık, size özel oluşturuldu.
                    </div>

                    <table role='presentation' align='center' cellpadding='0' cellspacing='0' border='0'>
                      <tr>
                        <td style='background:linear-gradient(135deg,#22c55e 0%,#34d399 100%);border-radius:18px;
                                   padding:22px 34px;text-align:center;min-width:330px;'>
                          <span style='font-size:32px;line-height:32px;font-weight:900;color:#ffffff;letter-spacing:6px;'>
                            {discountCode}
                          </span>
                        </td>
                      </tr>
                    </table>

                    <div style='font-size:13px;line-height:22px;color:#94a3b8;padding-top:18px;'>
                      Kodunuz 30 gün içinde geçerliliğini yitirecektir.
                    </div>
                  </td>
                </tr>
              </table>

              <!-- STEPS -->
              <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'
                     style='margin-top:28px;background-color:#f9fafb;border:1px solid #e5e7eb;border-radius:18px;'>
                <tr>
                  <td style='padding:24px 22px;'>
                    <div style='font-size:12px;line-height:12px;font-weight:800;letter-spacing:1px;text-transform:uppercase;color:#6b7280;padding-bottom:18px;'>
                      Sonraki Adımlar
                    </div>

                    <table role='presentation' width='100%' cellpadding='0' cellspacing='0' border='0'>
                      <tr>
                        <td valign='top' style='width:42px;padding-bottom:16px;'>
                          <table role='presentation' cellpadding='0' cellspacing='0' border='0'>
                            <tr>
                              <td style='width:30px;height:30px;background-color:#22c55e;border-radius:15px;
                                         text-align:center;vertical-align:middle;font-size:13px;font-weight:800;color:#ffffff;'>
                                1
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td valign='top' style='font-size:15px;line-height:28px;color:#374151;padding-bottom:16px;'>
                          Alış tarihinizde şubemize gelin, kimliğinizi ve ehliyet belgenizi ibraz edin.
                        </td>
                      </tr>

                      <tr>
                        <td valign='top' style='width:42px;padding-bottom:16px;'>
                          <table role='presentation' cellpadding='0' cellspacing='0' border='0'>
                            <tr>
                              <td style='width:30px;height:30px;background-color:#22c55e;border-radius:15px;
                                         text-align:center;vertical-align:middle;font-size:13px;font-weight:800;color:#ffffff;'>
                                2
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td valign='top' style='font-size:15px;line-height:28px;color:#374151;padding-bottom:16px;'>
                          Araç teslim tutanağını imzalayın ve aracınızı teslim alın.
                        </td>
                      </tr>

                      <tr>
                        <td valign='top' style='width:42px;'>
                          <table role='presentation' cellpadding='0' cellspacing='0' border='0'>
                            <tr>
                              <td style='width:30px;height:30px;background-color:#22c55e;border-radius:15px;
                                         text-align:center;vertical-align:middle;font-size:13px;font-weight:800;color:#ffffff;'>
                                3
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td valign='top' style='font-size:15px;line-height:28px;color:#374151;'>
                          İndirim kodunuzu bir sonraki rezervasyonunuzda ödeme adımında kullanın.
                        </td>
                      </tr>
                    </table>

                  </td>
                </tr>
              </table>

            </td>
          </tr>

          <!-- FOOTER -->
          <tr>
            <td style='background-color:#0f172a;padding:28px 30px;text-align:center;'>
              <div style='font-size:24px;line-height:24px;font-weight:800;color:#ffffff;padding-bottom:10px;'>
                Drive<span style='color:#22c55e;'>X</span>
              </div>
              <div style='font-size:12px;line-height:22px;color:#94a3b8;max-width:420px;margin:0 auto;'>
                Bu e-posta DriveX rezervasyon sistemi tarafından otomatik olarak gönderilmiştir.<br>
                Lütfen bu e-postayı yanıtlamayın.
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
}