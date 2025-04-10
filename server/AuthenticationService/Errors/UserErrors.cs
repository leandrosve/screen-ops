namespace AuthenticationService.Errors
{
    public static class UserErrors
    {

        public static class SignUp
        {
            public const string UserAlreadyExists = "user_already_exists";
            public const string FirstNameRequired = "first_name_required";
            public const string FirstNameMaxLength = "first_name_max_length";

            public const string LastNameRequired = "last_name_required";
            public const string LastNameMaxLength = "last_name_max_length";

            public const string EmailRequired = "email_required";
            public const string EmailInvalid = "email_invalid";
            public const string EmailMaxLength = "email_max_length";

            public const string PasswordRequired = "password_required";
            public const string PasswordMinLength = "password_min_length";
            public const string PasswordMaxLength = "password_max_length";
        }

        public static class Me
        {
            public const string UserNotFound = "user_not_found";
        }
        }
}
