using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUser.Request, Contracts.AddUser>();
        CreateMap<Contracts.AddUser.Response, AddUser.Response>();
        CreateMap<IdentifyUserForSignIn.Request, Contracts.IdentifyUserForSignIn>();
        CreateMap<Contracts.IdentifyUserForSignIn.Response, IdentifyUserForSignIn.Response>();
        CreateMap<SignInUserWithPassword.Request, Contracts.SignInUserWithPassword>();
        CreateMap<Contracts.SignInUserWithPassword.Response, SignInUserWithPassword.Response>();
    }
}