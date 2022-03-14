using System.Diagnostics.Contracts;
using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddPermission.Request, Contracts.AddPermission>();
        CreateMap<Contracts.AddPermission.Response, AddPermission.Response>();

        CreateMap<AddRole.Request, Contracts.AddRole>();
        CreateMap<Contracts.AddRole.Response, AddRole.Response>();

        CreateMap<AddUser.Request, Contracts.AddUser>();
        CreateMap<Contracts.AddUser.Response, AddUser.Response>();
        
        CreateMap<IdentifyUserForSignIn.Request, Contracts.IdentifyUserForSignIn>();
        CreateMap<Contracts.IdentifyUserForSignIn.Response, IdentifyUserForSignIn.Response>();

        CreateMap<SavePermissionsOfRole.Request, Contracts.SavePermissionsOfRole>();
        CreateMap<Contracts.SavePermissionsOfRole.Response, SavePermissionsOfRole.Response>();
        
        CreateMap<SignInUserWithPassword.Request, Contracts.SignInUserWithPassword>();
        CreateMap<Contracts.SignInUserWithPassword.Response, SignInUserWithPassword.Response>();
    }
}