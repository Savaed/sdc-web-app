using System.Net;

namespace SDCWebApp.ApiErrors
{
    public class NotFoundError : ApiError
    {
        public NotFoundError() : base(404, $"{HttpStatusCode.NotFound.ToString()}Error") { }

        public NotFoundError(string message) : base(404, $"{HttpStatusCode.NotFound.ToString()}Error", message) { }
    }
}

