namespace Nova.HRIS;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .RegisterCreated<Contracts.AddEmployee.Response, AddEmployee.Response>()
            .RegisterNotFound<Contracts.BarangayNotFound>()
            .RegisterNotFound<Contracts.BarangayNotInCity>()
            .RegisterNotFound<Contracts.CityNotFound>()
            .RegisterNotFound<Contracts.CityNotInProvince>()
            .RegisterNotFound<Contracts.CivilStatusNotFound>()
            .RegisterNotFound<Contracts.NationalityNotFound>()
            .RegisterNotFound<Contracts.ProvinceNotFound>()
            ;
    }
}
