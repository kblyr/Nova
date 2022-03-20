using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.HRIS.Handlers;

sealed class AddProvince_Handler : RequestHandler<AddProvince>
{
    readonly IDbContextFactory<ProvinceDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;

    public AddProvince_Handler(IDbContextFactory<ProvinceDbContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
    }

    public async Task<Response> Handle(AddProvince request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.Provinces.Exists(request.Name))
            return _mapper.Map<AddProvince, ProvinceAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var province = _mapper.Map<AddProvince, Province>(request);
        province.IsDeleted = false;
        province.InsertedById = auditInfo.UserId;
        province.InsertedOn = auditInfo.Timestamp;
        context.Provinces.Add(province);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddProvince.Response(province.Id);
    }
}
