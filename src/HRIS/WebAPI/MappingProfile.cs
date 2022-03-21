using AutoMapper;

namespace Nova.HRIS;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddBarangay.Request, Contracts.AddBarangay>();
        CreateMap<Contracts.AddBarangay.Response, AddBarangay.Response>();

        CreateMap<AddCity.Request, Contracts.AddCity>();
        CreateMap<Contracts.AddCity.Response, AddCity.Response>();

        CreateMap<AddEmployee.Request, Contracts.AddEmployee>();
        CreateMap<Contracts.AddEmployee.Response, AddEmployee.Response>();

        CreateMap<AddProvince.Request, Contracts.AddProvince>();
        CreateMap<Contracts.AddProvince.Response, AddProvince.Response>();
    }
}