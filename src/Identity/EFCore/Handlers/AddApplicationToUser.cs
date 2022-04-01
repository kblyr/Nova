using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.Identity.Handlers;

sealed class AddApplicationToUser_Handler : RequestHandler<AddApplicationToUser>
{
    readonly IDbContextFactory<UserApplicationDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;

    public AddApplicationToUser_Handler(IDbContextFactory<UserApplicationDbContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
    }

    public async Task<Response> Handle(AddApplicationToUser request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.Users.Exists(request.UserId) == false)
            return new UserNotFound(request.UserId);

        if (await context.Applications.Exists(request.ApplicationId) == false)
            return new ApplicationNotFound(request.ApplicationId);

        if (await context.UserApplications.Exists(request.UserId, request.ApplicationId))
            return _mapper.Map<AddApplicationToUser, UserApplicationAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var userApplication = _mapper.Map<AddApplicationToUser, UserApplication>(request);
        userApplication.IsDeleted = false;
        userApplication.InsertedById = auditInfo.UserId;
        userApplication.InsertedOn = auditInfo.Timestamp;
        context.UserApplications.Add(userApplication);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new AddApplicationToUser.Response(userApplication.Id);
    }
}
