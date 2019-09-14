namespace SDCWebApp.ApiErrors
{
    public class RegistrationError : ApiError
    {
        public RegistrationError() : base(200, "RegistrationError") { }

        public RegistrationError(string message) : base(200, "RegistrationError", message) { }
    }
}
