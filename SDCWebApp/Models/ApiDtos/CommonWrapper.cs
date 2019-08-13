using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SDCWebApp.Models.ApiDto
{
    /// <summary>
    /// Class for wrapping data in Http response.
    /// </summary>
    public sealed class CommonWrapper
    {
        /// <summary>
        /// Indicates whether request is successful or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Data in response. May be an empty list, list, object or null.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public object Data { get; set; } = null;

        /// <summary>
        /// An list of errors if are any.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public List<ApiError> Errors { get; set; } = null;
    }
}
