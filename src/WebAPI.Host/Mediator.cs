using AutoMapper;

namespace Nova;

public interface IApiMediator 
{
    Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo>? mutateRequest = null)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;

    Task<ObjectResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo>? mutateRequest = null)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;
}

sealed class ApiMediator : IApiMediator
{
    readonly MediatR.IMediator _mediator;
    readonly IMapper _mapper;
    readonly IResponseMapper _responseMapper;

    public ApiMediator(MediatR.IMediator mediator, IMapper mapper, IResponseMapper responseMapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _responseMapper = responseMapper;
    }

    public async Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo>? mutateRequest = null) 
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var requestTo = _mapper.Map<TRequestFrom, TRequestTo>(requestFrom);
        mutateRequest?.Invoke(requestTo);
        return await _mediator.Send(requestTo);
    }

    public async Task<ObjectResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo>? mutateRequest = null)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var response = await Send(requestFrom, mutateRequest);
        return _responseMapper.Map(response);
    }
}

public delegate T MutateRequest<T>(T request);