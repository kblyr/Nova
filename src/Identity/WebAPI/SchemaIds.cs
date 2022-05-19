namespace Nova.Identity;

static class RequestSchemaIds
{
    public const string CreateEmailVerificationCode = "req://identity.nova/create-email-verification-code";
}

static class ResponseSchemaIds
{
    public const string CreateEmailVerificationCode = "res://identity.nova/create-email-verification-code";
    public const string EmailVerificationCodeAlreadyCreated = "res://identity.nova/email-verification-code-already-created";
}