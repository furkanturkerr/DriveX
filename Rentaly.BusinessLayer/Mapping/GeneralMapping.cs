using AutoMapper;
using Rentaly.DtoLayer.AboutDtos;
using Rentaly.DtoLayer.ServiceDtos;
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
    }
}