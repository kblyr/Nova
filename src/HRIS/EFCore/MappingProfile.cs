using AutoMapper;

namespace Nova.HRIS;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddBarangay, Barangay>();
        CreateMap<AddCity, City>();
        CreateMap<AddEmployee, Employee>();
        CreateMap<AddProvince, Province>();
    }
}