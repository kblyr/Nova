using AutoMapper;

namespace Nova.HRIS;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddBarangay, BarangayAlreadyExists>();
        CreateMap<AddCity, CityAlreadyExists>();
        CreateMap<AddProvince, ProvinceAlreadyExists>();
    }
}