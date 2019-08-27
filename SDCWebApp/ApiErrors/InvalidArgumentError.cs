using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.ApiErrors
{
    public class InvalidArgumentError : ApiError
    {
        public InvalidArgumentError() : base(400, "InvalidArgumentError") { }

        public InvalidArgumentError(string message) : base(400, "InvalidArgumentError", message) { }
    }
}
