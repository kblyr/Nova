namespace Nova.Identity.Handlers;

// sealed class CreateEmailVerificationCodeHandler : IRequestHandler<CreateEmailVerificationCodeCommand>
// {
//     readonly IRequestClient<CreateEmailVerificationCodeCommand> _requestClient;

//     public CreateEmailVerificationCodeHandler(IRequestClient<CreateEmailVerificationCodeCommand> requestClient)
//     {
//         _requestClient = requestClient;
//     }

//     public async Task<IResponse> Handle(CreateEmailVerificationCodeCommand request, CancellationToken cancellationToken)
//     {
//         var response = await _requestClient.GetResponse<CreateEmailVerificationCodeCommand.Response>(request, cancellationToken);
//         return response.Message;
//     }
// }

sealed class CreateEmailVerificationCodeRequestedHandler : INotificationHandler<CreateEmailVerificationCodeRequestedEvent>
{
    readonly IBusAdapter _bus;

    public CreateEmailVerificationCodeRequestedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(CreateEmailVerificationCodeRequestedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}