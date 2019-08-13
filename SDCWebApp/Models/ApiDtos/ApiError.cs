using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.ApiDto
{
    public sealed class ApiError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
