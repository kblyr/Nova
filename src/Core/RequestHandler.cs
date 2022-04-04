namespace Nova;

public interface IRequestHandler<TRequest> : MediatR.IRequestHandler<TRequest, IResponse> where TRequest : IRequest { }