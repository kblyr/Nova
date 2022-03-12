using MediatR;
using Nova.Identity.Contracts;

namespace Nova.Identity.Handlers;

sealed class UserSignedIn_Handler : INotificationHandler<UserSignedIn>
{
    readonly IHttpContextAccessor _contextAccessor;

    public UserSignedIn_Handler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(UserSignedIn notification, CancellationToken cancellationToken)
    {
    }
}
