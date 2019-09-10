using System.Net;

namespace SDCWebApp.ApiErrors
{
    public class LoginError : ApiError
    {
        public LoginError() : base(200, "LoginError") { }

        public LoginError(string message) : base(200, "LoginError", message) { }
    }
}
