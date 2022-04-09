using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUser.Request, AddUserCommand>();
        CreateMap<AddUserCommand.Response, AddUser.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int32ToHashIdConverter, int>());

        CreateMap<AddUserPasswordLogin.Request, AddUserPasswordLoginCommand>();
        CreateMap<AddUserPasswordLoginCommand.Response, AddUserPasswordLogin.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int64ToHashIdConverter, long>());

        CreateMap<UserNotFoundResponse, UserNotFound.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int32ToHashIdConverter, int>());

        CreateMap<UserPasswordLoginAlreadyExistsResponse, UserPasswordLoginAlreadyExists.Response>()
            .ForMember(dest => dest.UserId, config => config.ConvertUsing<Int32ToHashIdConverter, int>());

        CreateMap<UserStatusNotFoundResponse, UserStatusNotFound.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int16ToHashIdConverter, short>());
    }
}