using AutoMapper;
using Rentaly.DtoLayer.AboutDtos;
using Rentaly.DtoLayer.BannerDtos;
using Rentaly.DtoLayer.ServiceDtos;
using Rentaly.DtoLayer.TestimonialDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        CreateMap<About, ResultAboutDto>().ReverseMap();
        CreateMap<About, GetAboutByIdDto>().ReverseMap();
        
        CreateMap<Service, ResultServiceDto>().ReverseMap();
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
        CreateMap<Service, CreateServiceDto>().ReverseMap();
        
        CreateMap<Banner, ResultBannerDto>().ReverseMap();
        CreateMap<Banner, CreateBannerDto>().ReverseMap();
        CreateMap<Banner, UpdateBannerDto>().ReverseMap();
        
        CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
        CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
        CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
    }
}