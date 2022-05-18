namespace Nova;

static class RequestSchemaIds
{
    public static class Email
    {
        public const string CreateVerificationCode = "req://identity.nova/email/create-verification-code";
    }
}

static class ResponseSchemaIds
{
    public static class Email
    {
        public const string CreateVerificationCode = "res://identity.nova/email/create-verification-code";
    }
}