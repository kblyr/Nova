using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddApplicationToUser.Request, Contracts.AddApplicationToUser>();
        CreateMap<Contracts.AddApplicationToUser.Response, AddApplicationToUser.Response>();

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

        CreateMap<SaveRolesAndPermissionsOfUser.Request, Contracts.SaveRolesAndPermissionsOfUser>();
        CreateMap<SaveRolesAndPermissionsOfUser.Request.RolesObj, Contracts.SaveRolesAndPermissionsOfUser.RolesObj>();
        CreateMap<SaveRolesAndPermissionsOfUser.Request.PermissionsObj, Contracts.SaveRolesAndPermissionsOfUser.PermissionsObj>();
        CreateMap<Contracts.SaveRolesAndPermissionsOfUser.Response, SaveRolesAndPermissionsOfUser.Response>();
        CreateMap<Contracts.SaveRolesAndPermissionsOfUser.Response.RolesObj, SaveRolesAndPermissionsOfUser.Response.RolesObj>();
        CreateMap<Contracts.SaveRolesAndPermissionsOfUser.Response.PermissionsObj, SaveRolesAndPermissionsOfUser.Response.PermissionsObj>();
        
        CreateMap<SignInUserWithPassword.Request, Contracts.SignInUserWithPassword>();
        CreateMap<Contracts.SignInUserWithPassword.Response, SignInUserWithPassword.Response>();
    }
}