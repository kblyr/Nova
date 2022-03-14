using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;
using Nova.Utilities;

namespace Nova.Identity.Handlers;

sealed class AddRole_Handler : RequestHandler<AddRole>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly RandomStringGenerator _codeGenerator;

    public AddRole_Handler(IDbContextFactory<DatabaseContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider currentAuditInfoProvider, RandomStringGenerator codeGenerator)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _codeGenerator = codeGenerator;
    }

    public async Task<Response> Handle(AddRole request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (request.DomainId.HasValue && request.DomainId.Value != 0 && await context.Domains.Exists(request.DomainId.Value) == false)
            return new DomainNotFound(request.DomainId.Value);

        if (request.ApplicationId.HasValue && request.ApplicationId.Value != 0 && await context.Applications.Exists(request.ApplicationId.Value) == false)
            return new ApplicationNotFound(request.ApplicationId.Value);

        var auditInfo = _currentAuditInfoProvider.Current;
        var role = _mapper.Map<AddRole, Role>(request);
        role.Code = await GenerateCode(context);
        role.IsDeleted = false;
        role.InsertedById = auditInfo.UserId;
        role.InsertedOn = auditInfo.Timestamp;

        return _mapper.Map<Role, AddRole.Response>(role);
    }

    async Task<string> GenerateCode(DatabaseContext context)
    {
        var code = _codeGenerator.Generate(10);

        if (await context.Roles.Where(role => role.Code == code).AnyAsync())
            return await GenerateCode(context);

        return code;
    }
}
