namespace Nova.Messaging;

public interface RequestHandler<TRequest> : MediatR.IRequestHandler<TRequest, Response> where TRequest : Request { }