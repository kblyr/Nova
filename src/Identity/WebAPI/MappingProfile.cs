using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEmailAddressToUser.Request, AddEmailAddressToUserCommand>();
        CreateMap<AddEmailAddressToUserCommand.Response, AddEmailAddressToUser.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int64ToHashIdConverter, long>(src => src.Id));

        CreateMap<AddPasswordLoginToUser.Request, AddPasswordLoginToUserCommand>();
        CreateMap<AddPasswordLoginToUserCommand.Response, AddPasswordLoginToUser.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int64ToHashIdConverter, long>(src => src.Id));

        CreateMap<AddUser.Request, AddUserCommand>();
        CreateMap<AddUserCommand.Response, AddUser.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int32ToHashIdConverter, int>(src => src.Id));

        CreateMap<UserNotFoundResponse, UserNotFound.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int32ToHashIdConverter, int>(src => src.Id));

        CreateMap<UserPasswordLoginAlreadyExistsResponse, UserPasswordLoginAlreadyExists.Response>()
            .ForMember(dest => dest.UserId, config => config.ConvertUsing<Int32ToHashIdConverter, int>(src => src.UserId));

        CreateMap<UserStatusNotFoundResponse, UserStatusNotFound.Response>()
            .ForMember(dest => dest.Id, config => config.ConvertUsing<Int16ToHashIdConverter, short>(src => src.Id));
    }
}