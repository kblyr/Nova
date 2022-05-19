namespace Nova.Identity;

static class RequestSchemaIds
{
    public const string CreateEmailVerificationCode = "req://identity.nova/create-email-verification-code";
    public const string VerifyEmail = "req://identity.nova/verify-email";
}

static class ResponseSchemaIds
{
    public const string CreateEmailVerificationCode = "res://identity.nova/create-email-verification-code";
    public const string EmailAddressNotFound = "res://identity.nova/email-address-not-found";
    public const string EmailVerificationCodeAlreadyCreated = "res://identity.nova/email-verification-code-already-created";
    public const string IncorrectEmailVerificationCode = "res://identity.nova/incorrect-email-verification-code";
    public const string VerifyEmail = "res://identity.nova/verify-email";
}