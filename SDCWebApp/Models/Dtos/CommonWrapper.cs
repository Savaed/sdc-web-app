using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class CommonWrapper
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public IEnumerable<ApiError> Errors { get; set; }
    }
}
