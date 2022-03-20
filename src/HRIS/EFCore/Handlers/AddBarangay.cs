using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.HRIS.Handlers;

sealed class AddBarangay_Handler : RequestHandler<AddBarangay>
{
    readonly IDbContextFactory<BarangayDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;

    public AddBarangay_Handler(IDbContextFactory<BarangayDbContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
    }

    public async Task<Response> Handle(AddBarangay request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (request.CityId.HasValue && await context.Cities.Exists(request.CityId.Value) == false)
            return new CityNotFound(request.CityId.Value);

        if (await context.Barangays.Exists(request.Name, request.CityId))
            return _mapper.Map<AddBarangay, BarangayAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var barangay = _mapper.Map<AddBarangay, Barangay>(request);
        barangay.IsDeleted = false;
        barangay.InsertedById = auditInfo.UserId;
        barangay.InsertedOn = auditInfo.Timestamp;
        context.Barangays.Add(barangay);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddBarangay.Response(barangay.Id);
    }
}
