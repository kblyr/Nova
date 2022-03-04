using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;
using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class AddUser_Handler : RequestHandler<AddUser>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;
    readonly IUserPasswordHash _userPasswordHash;

    public AddUser_Handler(IDbContextFactory<DatabaseContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper, IUserPasswordHash userPasswordHash)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
        _userPasswordHash = userPasswordHash;
    }

    public async Task<Response> Handle(AddUser request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();
        var auditInfo = _currentAuditInfoProvider.Current;

        if (await context.Users.UsernameExists(request.Username))
            return new UsernameAlreadyExists(request.Username);

        if (await context.UserStatuses.Exists(request.StatusId) == false)
            return new UserStatusNotFound(request.StatusId);

        var user = _mapper.Map<AddUser, User>(request);
        user.HashedPassword = _userPasswordHash.Compute(request.Password);
        user.IsDeleted = false;
        user.InsertedById = auditInfo.UserId;
        user.InsertedOn = auditInfo.Timestamp;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddUser.Response(user.Id);
    }
}