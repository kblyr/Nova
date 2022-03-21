using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.Identity.Handlers;

sealed class AddPermission_Handler : RequestHandler<AddPermission>
{
    readonly IDbContextFactory<PermissionDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;

    public AddPermission_Handler(IDbContextFactory<PermissionDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider currentAuditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _currentAuditInfoProvider = currentAuditInfoProvider;
    }

    public async Task<Response> Handle(AddPermission request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (request.DomainId.HasValue && request.DomainId.Value != 0 && await context.Domains.Exists(request.DomainId.Value) == false)
            return new DomainNotFound(request.DomainId.Value);

        if (request.ApplicationId.HasValue && request.ApplicationId.Value != 0)
        {
            var application = await GetApplication(context, request.ApplicationId.Value);

            if (application is null)
                return new ApplicationNotFound(request.ApplicationId.Value);

            if (application.DomainId != request.DomainId)
                return new ApplicationNotInDomain(application.Id, request.DomainId);
        }

        if (await context.Permissions.Exists(request.Name, request.DomainId, request.ApplicationId))
            return _mapper.Map<AddPermission, PermissionAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var permission = _mapper.Map<AddPermission, Permission>(request);
        permission.IsDeleted = false;
        permission.InsertedById = auditInfo.UserId;
        permission.InsertedOn = auditInfo.Timestamp;
        context.Permissions.Add(permission);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return _mapper.Map<Permission, AddPermission.Response>(permission);
    }
    
    static async Task<Application> GetApplication(PermissionDbContext context, int id)
    {
        return await context.Applications
            .AsNoTracking()
            .Where(application => application.Id == id)
            .Select(application => new Application
            {
                Id = application.Id,
                DomainId = application.DomainId
            })
            .SingleOrDefaultAsync();
    }
}
