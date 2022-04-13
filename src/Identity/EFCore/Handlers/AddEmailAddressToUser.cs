using AutoMapper;
using MediatR;

namespace Nova.Identity.Handlers;

sealed class AddEmailAddressToUserHandler : IRequestHandler<AddEmailAddressToUserCommand>
{
    readonly IDbContextFactory<UserEmailAddressDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IMapper _mapper;
    readonly IMediator _mediator;

    public AddEmailAddressToUserHandler(IDbContextFactory<UserEmailAddressDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IMapper mapper, IMediator mediator)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(AddEmailAddressToUserCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.UserEmailAddresses.Exists(request.EmailAddress))
            return new UserEmailAddressAlreadyExistsResponse(request.EmailAddress);

        if (await context.Users.Exists(request.UserId) == false)
            return new UserNotFoundResponse(request.UserId);

        var auditInfo = _auditInfoProvider.Current;
        var userEmailAddress = _mapper.Map<AddEmailAddressToUserCommand, UserEmailAddress>(request);
        userEmailAddress.IsVerified = false;
        userEmailAddress.IsDeleted = false;
        userEmailAddress.InsertedById = auditInfo.UserId;
        userEmailAddress.InsertedOn = auditInfo.Timestamp;
        context.UserEmailAddresses.Add(userEmailAddress);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        await _mediator.Publish(_mapper.Map<UserEmailAddress, UserEmailAddressAddressAddedEvent>(userEmailAddress));
        return new AddEmailAddressToUserCommand.Response(userEmailAddress.Id);
    }
}
