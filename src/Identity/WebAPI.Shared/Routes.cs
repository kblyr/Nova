namespace Nova.Identity;

public static class ControllerRoutes
{
    public const string User = "user";
}

public static class ActionRoutes
{
    public static class User
    {
        public const string Add = "";
        public const string AddPasswordLogin = "{id}/password-login";
        public const string AddEmailAddress = "{id}/email-address";
    }
}
