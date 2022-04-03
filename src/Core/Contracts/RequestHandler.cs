namespace Nova.Contracts;

public interface IRequestHandler<TRequest> : MediatR.IRequestHandler<TRequest, IResponse> where TRequest : IRequest { }