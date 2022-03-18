using MediatR.Pipeline;

namespace Nova.Messaging;

public interface RequestPostProcessor<TRequest> : IRequestPostProcessor<TRequest, Response> where TRequest : Request { }