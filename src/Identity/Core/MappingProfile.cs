using AutoMapper;
using Nova.Authentication.ClaimTypes;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddPermission, PermissionAlreadyExists>();
        CreateMap<AddRole, RoleAlreadyExists>();
        CreateMap<GenerateAccessToken, GetAccessTokenPayload>();
        CreateMap<GetAccessTokenPayload.Response.UserObj, SessionClaimType.UserObj>();
        CreateMap<GetAccessTokenPayload.Response.ApplicationObj, SessionClaimType.ApplicationObj>();
        CreateMap<GetAccessTokenPayload.Response.DomainObj, SessionClaimType.DomainObj>();
    }
}