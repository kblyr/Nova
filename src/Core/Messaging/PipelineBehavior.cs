using MediatR;

namespace Nova.Messaging;

public interface PipelineBehavior<TRequest> : IPipelineBehavior<TRequest, Response> where TRequest : Request { }