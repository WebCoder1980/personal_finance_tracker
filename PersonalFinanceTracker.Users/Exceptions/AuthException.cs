namespace Users.Exceptions
{
    public class AuthException : Exception
    {
        public string Code { get; }

        public AuthException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class InvalidCredentialsException : AuthException
    {
        public InvalidCredentialsException()
            : base("invalid_credentials", "Invalid credentials.")
        {
        }
    }

    public class UserAlreadyExistsException : AuthException
    {
        public UserAlreadyExistsException()
            : base("user_already_exists", "User is already exists.")
        {
        }
    }
}
