using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUser, User>();
        CreateMap<User, IdentifyUserForSignIn.Response>();
        CreateMap<User, SignInUserWithPassword.Response>();
    }
}