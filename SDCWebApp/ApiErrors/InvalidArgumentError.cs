using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.ApiErrors
{
    public class InvalidParameterError : ApiError
    {
        public InvalidParameterError() : base(400, "InvalidArgumentError") { }

        public InvalidParameterError(string message) : base(400, "InvalidArgumentError", message) { }
    }
}
