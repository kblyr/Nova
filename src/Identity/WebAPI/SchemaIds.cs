namespace Nova.Identity;

static class RequestSchemaIds
{
    public const string CreateEmailVerificationCode = "req://identity.nova/create-email-verification-code";
    public const string SignUpUser = "req://identity.nova/sign-up-user";
    public const string VerifyEmail = "req://identity.nova/verify-email";
    public const string VerifyUserEmail = "req://identity.nova/verify-user-email";
}

static class ResponseSchemaIds
{
    public const string CreateEmailVerificationCode = "res://identity.nova/create-email-verification-code";
    public const string EmailAddressNotFound = "res://identity.nova/email-address-not-found";
    public const string EmailVerificationCodeAlreadyCreated = "res://identity.nova/email-verification-code-already-created";
    public const string IncorrectEmailVerificationCode = "res://identity.nova/incorrect-email-verification-code";
    public const string IncorrectUserEmailVerificationCode = "res://identity.nova/incorrect-user-email-verification-code";
    public const string SignUpUser = "res://identity.nova/sign-up-user";
    public const string UserEmailAddressAlreadyExists = "res://identity.nova/user-email-address-already-exists";
    public const string UserEmailAddressNotFound = "res://identity.nova/user-email-address-not-found";
    public const string VerifyEmail = "res://identity.nova/verify-email";
    public const string VerifyUserEmail = "res://identity.nova/verify-user-email";
}