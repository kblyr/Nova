namespace Nova.HRIS;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .RegisterCreated<Contracts.AddBarangay.Response, AddBarangay.Response>()
            .RegisterCreated<Contracts.AddCity.Response, AddCity.Response>()
            .RegisterCreated<Contracts.AddEmployee.Response, AddEmployee.Response>()
            .RegisterCreated<Contracts.AddProvince.Response, AddProvince.Response>()
            .RegisterConflict<Contracts.BarangayAlreadyExists>()
            .RegisterNotFound<Contracts.BarangayNotFound>()
            .RegisterNotFound<Contracts.BarangayNotInCity>()
            .RegisterConflict<Contracts.CityAlreadyExists>()
            .RegisterNotFound<Contracts.CityNotFound>()
            .RegisterNotFound<Contracts.CityNotInProvince>()
            .RegisterNotFound<Contracts.CivilStatusNotFound>()
            .RegisterConflict<Contracts.EmployeeAlreadyExists>()
            .RegisterNotFound<Contracts.NationalityNotFound>()
            .RegisterConflict<Contracts.ProvinceAlreadyExists>()
            .RegisterNotFound<Contracts.ProvinceNotFound>()
            ;
    }
}
