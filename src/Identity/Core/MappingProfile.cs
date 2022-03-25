#nullable disable
using AutoMapper;
using Nova.Authentication;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddApplicationToUser, UserApplicationAlreadyExists>();
        CreateMap<AddPermission, PermissionAlreadyExists>();
        CreateMap<AddRole, RoleAlreadyExists>();
        CreateMap<GenerateAccessToken, GetAccessTokenPayload>();
        CreateMap<GetAccessTokenPayload.Response, Session>()
            .ForMember(dest => dest.UserId, config => config.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.Username, config => config.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.ApplicationId, config => config.MapFrom(src => src.Application.Id))
            .ForMember(dest => dest.DomainId, config => config.MapFrom(src => src.Application.Domain.Id));
    }
}