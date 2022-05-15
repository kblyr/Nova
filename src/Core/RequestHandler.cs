namespace Nova;

public interface IRequestHandler<T> : MediatR.IRequestHandler<T, IResponse> where T : IRequest { }