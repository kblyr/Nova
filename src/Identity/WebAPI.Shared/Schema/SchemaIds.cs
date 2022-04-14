namespace Nova.Identity.Schema;

static class SchemaIds
{
    public static class AddUser
    {
        public const string Request = "request://identity.nova/user/add";
        public const string Response = "response://identity.nova/user/add";
    }


    public static class AddEmailAddressToUser
    {
        public const string Request = "request://identity.nova/user/addEmailAddress";
        public const string Response = "response://identity.nova/user/addEmailAddress";
    }

    public static class AddPasswordLoginToUser
    {
        public const string Request = "request://identity.nova/user/addPasswordLogin";
        public const string Response = "response://identity.nova/user/addPasswordLogin";
    }

    public static class UserEmailAddressAlreadyExists
    {
        public const string Response = "response://identity.nova/userEmailAddressAlreadyExists";
    }

    public static class UserNotFound
    {
        public const string Response = "response://identity.nova/userNotFound";
    }

    public static class UserPasswordLoginAlreadyExists
    {
        public const string Response = "response://identity.nova/userPasswordLoginAlreadyExists";
    }

    public static class UserStatusNotFound
    {
        public const string Response = "response://identity.nova/userStatusNotFound";
    }
}