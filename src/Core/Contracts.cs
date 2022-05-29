namespace Nova;

public interface IRequest : MediatR.IRequest<IResponse> { }

public interface IResponse { }

public interface IFailedResponse : IResponse { }

public interface IRequestHandler<T> : MediatR.IRequestHandler<T, IResponse> where T : IRequest { }