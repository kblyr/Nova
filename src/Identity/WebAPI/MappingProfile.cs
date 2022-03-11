using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUser.Request, Contracts.AddUser>();
        CreateMap<Contracts.AddUser.Response, AddUser.Response>();
    }
}