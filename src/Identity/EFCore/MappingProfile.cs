using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddPermission, Permission>();
        CreateMap<AddRole, Role>();
        CreateMap<AddUser, User>();

        CreateMap<Permission, AddPermission.Response>();
        CreateMap<Role, AddRole.Response>();
        CreateMap<User, IdentifyUserForSignIn.Response>();
        CreateMap<User, SignInUserWithPassword.Response>();
    }
}