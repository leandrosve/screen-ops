namespace AuthenticationService.Errors
{
    public static class AuthErrors
    {

        public static class Login
        {
            public const string InvalidCredentials = "invalid_credentials";
            public const string EmailRequired = "email_required";
            public const string EmailInvalid = "email_invalid";
            public const string PasswordRequired = "password_required";
            public const string PasswordInvalid = "password_invalid";
        }
    }
}
