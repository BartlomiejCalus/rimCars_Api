using AutoMapper;
using rimCars_Api.Entities;
using rimCars_Api.Models;

namespace rimCars_Api
{
    public class SalonMappingProfile : Profile
    {
        public SalonMappingProfile()
        {
            CreateMap<Salon, SalonDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street));

            CreateMap<Rim, RimsDto>();

            CreateMap<AddSalonDto, Salon>()
                .ForMember(s => s.Address, c => c.MapFrom(dto => new Address() { City = dto.City, Street = dto.Street }));

        }
    }
}
