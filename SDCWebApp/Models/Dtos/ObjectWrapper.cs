using Newtonsoft.Json;
using SDCWebApp.ApiErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    /// <summary>
    /// Encapsulates common server response field such as Success, Data and Error.
    /// </summary>
    public class ResponseWrapper
    {
        public object Data { get; set; }
        public ApiError Error { get; set; }


        public ResponseWrapper(object data, ApiError error)
        {
            Data = data;
            Error = error;
        }

        public ResponseWrapper(object data)
        {
            Data = data;
            Error = new object() as ApiError;
        }

        public ResponseWrapper(ApiError error)
        {
            Data = new object();
            Error = error;
        }        
    }
}
