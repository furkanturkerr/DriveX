using AutoMapper;
using Rentaly.DtoLayer.AboutDtos;
using Rentaly.DtoLayer.BannerDtos;
using Rentaly.DtoLayer.BlogDtos;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.BrandDtos;
using Rentaly.DtoLayer.CarCategoryDtos;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.DtoLayer.ContactDtos;
using Rentaly.DtoLayer.FaqDtos;
using Rentaly.DtoLayer.FooterDtos;
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
        
        CreateMap<Faq, ResultFaqDto>().ReverseMap();
        CreateMap<Faq, UpdateFaqDto>().ReverseMap();
        CreateMap<Faq, CreateFaqDto>().ReverseMap();
        
        CreateMap<Contact, ResultContactDto>().ReverseMap();
        CreateMap<Contact, UpdateContactDto>().ReverseMap();
        CreateMap<Contact, CreateContactDto>().ReverseMap();
        
        CreateMap<Footer, ResultFooterDto>().ReverseMap();
        CreateMap<Footer, UpdateFooterDto>().ReverseMap();
        CreateMap<Footer, CreateFooterDto>().ReverseMap();
        
        CreateMap<Blog, ResultBlogDto>().ReverseMap();
        CreateMap<Blog, UpdateBlogDto>().ReverseMap();
        CreateMap<Blog, CreateBlogDto>().ReverseMap();
        CreateMap<Blog, ResultBlogWithCategoryDto>().ReverseMap();
        
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        
        CreateMap<Branch, ResultBranchDto>().ReverseMap();
        CreateMap<Branch, UpdateBranchDto>().ReverseMap();
        CreateMap<Branch, CreateBranchDto>().ReverseMap();
        
        CreateMap<Brand, ResultBrandDto>().ReverseMap();
        CreateMap<Brand, UpdateBrandDto>().ReverseMap();
        CreateMap<Brand, CreateBrandDto>().ReverseMap();
        
        CreateMap<CarCategory, ResultCarCategoryDto>().ReverseMap();
        CreateMap<CarCategory, UpdateCarCategoryDto>().ReverseMap();
        CreateMap<CarCategory, CreateCarCategoryDto>().ReverseMap();
    }
}