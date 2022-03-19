using AutoMapper;

namespace Nova.HRIS;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEmployee, Employee>();
    }
}