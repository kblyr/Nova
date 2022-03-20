using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Auditing;
using Nova.Utilities;

namespace Nova.HRIS.Handlers;

sealed class AddEmployee_Handler : RequestHandler<AddEmployee>
{
    readonly IDbContextFactory<EmployeeDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;
    readonly IMapper _mapper;
    readonly IFullNameBuilder _fullNameBuilder;
    readonly IFullAddressBuilder _fullAddressBuilder;

    public AddEmployee_Handler(IDbContextFactory<EmployeeDbContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider, IMapper mapper, IFullNameBuilder fullNameBuilder, IFullAddressBuilder fullAddressBuilder)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
        _mapper = mapper;
        _fullNameBuilder = fullNameBuilder;
        _fullAddressBuilder = fullAddressBuilder;
    }

    public async Task<Response> Handle(AddEmployee request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();
        Province province = null;
        City city = null;
        Barangay barangay = null;

        if (await context.Employees.Exists(request.FirstName, request.MiddleName, request.LastName, request.NameSuffix, request.Sex, request.BirthDate))
            return _mapper.Map<AddEmployee, EmployeeAlreadyExists>(request);

        if (request.CivilStatusId.HasValue && await context.CivilStatuses.Exists(request.CivilStatusId.Value) == false)
            return new CivilStatusNotFound(request.CivilStatusId.Value);

        if (request.NationalityId.HasValue && await context.Nationalities.Exists(request.NationalityId.Value) == false)
            return new NationalityNotFound(request.NationalityId.Value);

        if (request.ProvinceId.HasValue)
        {
            province = await GetProvince(context, request.ProvinceId.Value);

            if (province is null)
                return new ProvinceNotFound(request.ProvinceId.Value);
        }

        if (request.CityId.HasValue) 
        {
            city = await GetCity(context, request.CityId.Value);

            if (city is null)
                return new CityNotFound(request.CityId.Value);

            if (city.ProvinceId != request.ProvinceId)
                return new CityNotInProvince(request.CityId.Value, request.ProvinceId);
        }

        if (request.BarangayId.HasValue)
        {
            barangay = await GetBarangay(context, request.BarangayId.Value);

            if (barangay is null)
                return new BarangayNotFound(request.BarangayId.Value);

            if (barangay.CityId != request.CityId)
                return new BarangayNotInCity(request.BarangayId.Value, request.CityId);
        }

        var auditInfo = _currentAuditInfoProvider.Current;
        var employee = _mapper.Map<AddEmployee, Employee>(request);
        employee.FullName = _fullNameBuilder.Build(employee.FirstName, employee.MiddleName, employee.LastName, employee.NameSuffix, Defaults.FullNameFormat);
        employee.FullAddress = _fullAddressBuilder.Build(employee.Address, barangay?.Name, city?.Name, province?.Name);
        employee.IsDeleted = false;
        employee.InsertedById = auditInfo.UserId;
        employee.InsertedOn = auditInfo.Timestamp;

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddEmployee.Response(employee.Id);
    }

    static async Task<Province> GetProvince(EmployeeDbContext context, short id)
    {
        return await context.Provinces
            .AsNoTracking()
            .Where(province => province.Id == id && !province.IsDeleted)
            .Select(province => new Province
            {
                Id = province.Id,
                Name = province.Name
            })
            .SingleOrDefaultAsync();
    }

    static async Task<City> GetCity(EmployeeDbContext context, short id)
    {
        return await context.Cities
            .AsNoTracking()
            .Where(city => city.Id == id && !city.IsDeleted)
            .Select(city => new City
            {
                Id = city.Id,
                Name = city.Name,
                ProvinceId = city.ProvinceId
            })
            .SingleOrDefaultAsync();
    }

    static async Task<Barangay> GetBarangay(EmployeeDbContext context, short id)
    {
        return await context.Barangays
            .AsNoTracking()
            .Where(barangay => barangay.Id == id && !barangay.IsDeleted)
            .Select(barangay => new Barangay
            {
                Id = barangay.Id,
                Name = barangay.Name,
                CityId = barangay.CityId
            })
            .SingleOrDefaultAsync();
    }
}
