namespace Nova.Schema;

public static class CreateEmailVerificationCodeRequested
{
    public record Request : IApiRequest
    {
        public string EmailAddress { get; init; } = "";
    }

    public record Response : IApiResponse
    {
    }
}