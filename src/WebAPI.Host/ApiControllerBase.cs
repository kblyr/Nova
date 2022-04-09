using HashidsNet;

namespace Nova;

public abstract class ApiControllerBase : ControllerBase
{
    IApiMediator? _mediator;
    protected IApiMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IApiMediator>();

    IHashids? _hashids;
    protected IHashids Hashids => _hashids ??= HttpContext.RequestServices.GetRequiredService<IHashids>(); 
}