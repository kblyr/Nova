using Nova.Utilities;

namespace Nova.HRIS.Handlers;

sealed class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand>
{
    readonly IDbContextFactory<HRISDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IFullNameBuilder _fullNameBuilder;
    readonly AddressTypesLookup _addressTypes;
    readonly IAddressFullNameBuilder _addressFullNameBuilder;

    public AddEmployeeHandler(IDbContextFactory<HRISDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IFullNameBuilder fullNameBuilder, IOptions<AddressTypesLookup> addressTypes, IAddressFullNameBuilder addressFullNameBuilder)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _fullNameBuilder = fullNameBuilder;
        _addressTypes = addressTypes.Value;
        _addressFullNameBuilder = addressFullNameBuilder;
    }

    public async Task<IResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        
        if (await context.Employees.DoesExists(request.FirstName, request.LastName, request.BirthDate, cancellationToken))
        {
            return request.Adapt<AddEmployeeCommand, EmployeeAlreadyExistsResponse>();
        }

        if (await context.CivilStatuses.DoesExists(request.CivilStatusId, cancellationToken) == false)
        {
            return new CivilStatusNotFoundResponse { Id = request.CivilStatusId };
        }

        if (await context.Citizenships.DoesExists(request.CitizenshipId, cancellationToken) == false)
        {
            return new CitizenshipNotFoundResponse { Id = request.CitizenshipId };
        }

        if (await context.EmploymentStatuses.DoesExists(request.EmploymentStatusId, cancellationToken) == false)
        {
            return new EmploymentStatusNotFoundResponse { Id = request.EmploymentStatusId };
        }

        var auditInfo = _auditInfoProvider.Current;
        var fullName = _fullNameBuilder.Build(
            firstName: request.FirstName,
            middleName: request.MiddleName,
            lastName: request.LastName,
            nameSuffix: request.NameSuffix
        );
        var employee = request.Adapt<AddEmployeeCommand, Employee>() with
        {
            FullName = fullName,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.Employees.Add(employee);
        await context.SaveChangesAsync(cancellationToken);

        if (request.PermanentAddress is not null)
        {
            var response = await SaveEmployeeAddress(context, employee.Id, _addressTypes.Permanent, request.PermanentAddress, auditInfo, cancellationToken);
            if (response is not null)
            {
                return response;
            }
        }

        if (request.ResidentialAddress is not null)
        {
            var response = await SaveEmployeeAddress(context, employee.Id, _addressTypes.Residential, request.ResidentialAddress, auditInfo, cancellationToken);
            if (response is not null)
            {
                return response;
            }
        }

        if (request.Employment is not null)
        {
            var response = await SaveEmployment(context, employee.Id, request.Employment, auditInfo, cancellationToken);
            if (response is not null)
            {
                return response;
            }
        }

        if (request.SalaryGradeStep is not null)
        {
            var response = await SaveEmployeeSalaryGradeStep(context, employee.Id, request.SalaryGradeStep, auditInfo, cancellationToken);
            if (response is not null)
            {
                return response;
            }
        }

        await transaction.CommitAsync(cancellationToken);
        return new AddEmployeeCommand.Response { Id = employee.Id };
    }

    async Task<IResponse?> SaveEmployeeAddress(HRISDbContext context, int employeeId, short typeId, AddEmployeeCommand.AddressObj requestAddress, AuditInfo auditInfo, CancellationToken cancellationToken)
    {
        var province = requestAddress.ProvinceId is null ? null : await GetProvince(context, requestAddress.ProvinceId.Value, cancellationToken);
        var city = requestAddress.CityId is null ? null : await GetCity(context, requestAddress.CityId.Value, cancellationToken);

        if (city is not null && city.ProvinceId != province?.Id)
        {
            return new CityNotInProvinceResponse 
            { 
                CityId = city.Id, 
                ProvinceId = province?.Id
            };
        }

        var barangay = requestAddress.BarangayId is null ? null : await GetBarangay(context, requestAddress.BarangayId.Value, cancellationToken);

        if (barangay is not null && barangay.CityId != city?.Id)
        {
            return new BarangayNotInCityResponse
            {
                BarangayId = barangay.Id,
                CityId = city?.Id
            };
        }

        var fullName = _addressFullNameBuilder.Build(
            unitRoomNumber: requestAddress.UnitRoomNumber ?? "",
            houseNumber: requestAddress.HouseNumber ?? "",
            building: requestAddress.Building ?? "",
            blockNumber: requestAddress.BlockNumber ?? "",
            lotNumber: requestAddress.LotNumber ?? "",
            phaseNumber: requestAddress.PhaseNumber ?? "",
            street: requestAddress.Street ?? "",
            subdivisionVillage: requestAddress.SubdivisionVillage ?? "",
            barangay: barangay?.Name ?? "",
            city: city?.Name ?? "",
            province: province?.Name ?? "",
            zipCode: requestAddress.ZipCode ?? ""
        );
        var employeeAddress = requestAddress.Adapt<AddEmployeeCommand.AddressObj, EmployeeAddress>() with
        {
            EmployeeId = employeeId,
            TypeId = typeId,
            BarangayId = barangay?.Id,
            CityId = city?.Id,
            ProvinceId = province?.Id,
            FullName = fullName,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.EmployeeAddresses.Add(employeeAddress);
        await context.SaveChangesAsync(cancellationToken);
        return null;
    }

    async Task<Province?> GetProvince(HRISDbContext context, int id, CancellationToken cancellationToken)
    {
        return await context.Provinces.AsNoTracking()
            .Where(province => province.Id == id && !province.IsDeleted)
            .Select(province => new Province {
                Id = province.Id,
                Name = province.Name
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    async Task<City?> GetCity(HRISDbContext context, int id, CancellationToken cancellationToken)
    {
        return await context.Cities.AsNoTracking()
            .Where(city => city.Id == id && !city.IsDeleted)
            .Select(city => new City {
                Id = city.Id,
                Name = city.Name,
                ProvinceId = city.ProvinceId
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    async Task<Barangay?> GetBarangay(HRISDbContext context, int id, CancellationToken cancellationToken)
    {
        return await context.Barangays.AsNoTracking()
            .Where(barangay => barangay.Id == id && !barangay.IsDeleted)
            .Select(barangay => new Barangay {
                Id = barangay.Id,
                Name = barangay.Name,
                CityId = barangay.CityId
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    async Task<IResponse?> SaveEmployment(HRISDbContext context, int employeeId, AddEmployeeCommand.EmploymentObj requestEmployment, AuditInfo auditInfo, CancellationToken cancellationToken)
    {
        if (await context.EmploymentTypes.DoesExists(requestEmployment.TypeId, cancellationToken) == false)
        {
            return new EmploymentTypeNotFoundResponse { Id = requestEmployment.TypeId };
        }

        if (await context.Offices.DoesExists(requestEmployment.OfficeId, cancellationToken) == false)
        {
            return new OfficeNotFoundResponse { Id = requestEmployment.OfficeId };
        }

        if (await context.Positions.DoesExists(requestEmployment.PositionId, cancellationToken) == false)
        {
            return new PositionNotFoundResponse { Id = requestEmployment.PositionId };
        }

        var employment = requestEmployment.Adapt<AddEmployeeCommand.EmploymentObj, Employment>() with
        {
            EmployeeId = employeeId,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.Employments.Add(employment);
        await context.SaveChangesAsync(cancellationToken);
        return null;
    }

    async Task<IResponse?> SaveEmployeeSalaryGradeStep(HRISDbContext context, int employeeId, AddEmployeeCommand.SalaryGradeStepObj requestSalaryGradeStep, AuditInfo auditInfo, CancellationToken cancellationToken)
    {
        if (await context.SalaryGradeSteps.IsValid(requestSalaryGradeStep.Grade, requestSalaryGradeStep.Step, requestSalaryGradeStep.EffectivityBeginDate, requestSalaryGradeStep.EffectivityEndDate, cancellationToken) == false)
        {
            return requestSalaryGradeStep.Adapt<AddEmployeeCommand.SalaryGradeStepObj, InvalidSalaryGradeStepResponse>();
        }

        var employeeSalaryGradeStep = requestSalaryGradeStep.Adapt<AddEmployeeCommand.SalaryGradeStepObj, EmployeeSalaryGradeStep>() with
        {
            EmployeeId = employeeId,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.EmployeeSalaryGradeSteps.Add(employeeSalaryGradeStep);
        await context.SaveChangesAsync(cancellationToken);
        return null;
    }
}
