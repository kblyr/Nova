using AutoMapper;
using MediatR;

namespace Nova.Messaging;

public class MappedMediator
{
    readonly IMediator _mediator;
    readonly IMapper _mapper;

    public MappedMediator(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Response> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom) where TRequestTo : Request
    {
        var requestTo = _mapper.Map<TRequestFrom, TRequestTo>(requestFrom);
        return await _mediator.Send(requestTo);
    }

    public async Task<Response> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, Func<TRequestTo, TRequestTo> mutateRequest) where TRequestTo : Request
    {
        var requestTo = _mapper.Map<TRequestFrom, TRequestTo>(requestFrom);
        return await _mediator.Send(mutateRequest(requestTo));
    }
}