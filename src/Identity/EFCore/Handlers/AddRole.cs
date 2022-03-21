using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.Identity.Handlers;

sealed class AddRole_Handler : RequestHandler<AddRole>
{
    readonly IDbContextFactory<RoleDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;

    public AddRole_Handler(IDbContextFactory<RoleDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider currentAuditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _currentAuditInfoProvider = currentAuditInfoProvider;
    }

    public async Task<Response> Handle(AddRole request, CancellationToken cancellationToken)
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

        if (await context.Roles.Exists(request.Name, request.DomainId, request.ApplicationId))
            return _mapper.Map<AddRole, RoleAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var role = _mapper.Map<AddRole, Role>(request);
        role.IsDeleted = false;
        role.InsertedById = auditInfo.UserId;
        role.InsertedOn = auditInfo.Timestamp;
        context.Roles.Add(role);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return _mapper.Map<Role, AddRole.Response>(role);
    }
    
    static async Task<Application> GetApplication(RoleDbContext context, int id)
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
