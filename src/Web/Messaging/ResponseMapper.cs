using AutoMapper;

namespace Nova.Messaging;

public class ResponseMapper
{
    readonly IMapper _mapper;

    public ResponseMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public object? Map<TSuccessResponse, TApiResponse>(Response response) where TSuccessResponse : Response
    {
        if (response is TSuccessResponse successResponse)
            return _mapper.Map<TSuccessResponse, TApiResponse>(successResponse);

        if (response is FailedResponse failedResponse)
            return new ApiFailedResponse(response.GetType().Name, failedResponse);

        throw new InvalidOperationException("Unsupported response type");
    }
}