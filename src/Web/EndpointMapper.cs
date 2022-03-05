using Microsoft.AspNetCore.Routing;

namespace Nova;

public interface EndpointMapper
{
    void Map(IEndpointRouteBuilder builder);
}