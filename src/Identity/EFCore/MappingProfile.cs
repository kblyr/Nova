using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddRole, Role>();
        CreateMap<AddUser, User>();

        CreateMap<Role, AddRole.Response>();
        CreateMap<User, IdentifyUserForSignIn.Response>();
        CreateMap<User, SignInUserWithPassword.Response>();
    }
}