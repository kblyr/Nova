using AutoMapper;

namespace Nova.HRIS;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEmployee.Request, Contracts.AddEmployee>();
        CreateMap<Contracts.AddEmployee.Response, AddEmployee.Response>();
    }
}