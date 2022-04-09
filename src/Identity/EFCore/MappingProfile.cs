using AutoMapper;

namespace Nova.Identity;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUserCommand, User>();
        CreateMap<AddUserEmailAddressCommand, UserEmailAddress>();
        
        CreateMap<User, UserAddedEvent>();
        CreateMap<UserEmailAddress, UserEmailAddressAddressAddedEvent>();
    }
}