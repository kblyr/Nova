using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.HRIS.Handlers;

sealed class AddCity_Handler : RequestHandler<AddCity>
{
    readonly IDbContextFactory<CityDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;

    public AddCity_Handler(IDbContextFactory<CityDbContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
    }

    public async Task<Response> Handle(AddCity request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (request.ProvinceId.HasValue && await context.Provinces.Exists(request.ProvinceId.Value) == false)
            return new ProvinceNotFound(request.ProvinceId.Value);

        if (await context.Cities.Exists(request.Name, request.ProvinceId))
            return _mapper.Map<AddCity, CityAlreadyExists>(request);

        var auditInfo = _currentAuditInfoProvider.Current;
        var city = _mapper.Map<AddCity, City>(request);
        city.IsDeleted = false;
        city.InsertedById = auditInfo.UserId;
        city.InsertedOn = auditInfo.Timestamp;
        context.Cities.Add(city);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddCity.Response(city.Id);
    }
}
