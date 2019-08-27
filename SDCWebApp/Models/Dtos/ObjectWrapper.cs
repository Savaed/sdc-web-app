using Newtonsoft.Json;
using SDCWebApp.ApiErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    /// <summary>
    /// Encapsulates common server response field such as Data and Error.
    /// </summary>
    public class ResponseWrapper
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; } = null;
        public ApiError Error { get; set; }


        public ResponseWrapper(object data, ApiError error)
        {
            Data = data;
            Error = error;
        }

        public ResponseWrapper(object data)
        {
            Data = data;
        }

        public ResponseWrapper(ApiError error)
        {
            Error = error;
        }        
    }
}
