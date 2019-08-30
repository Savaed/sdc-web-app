using Newtonsoft.Json;

namespace SDCWebApp.ApiErrors
{
    public class ApiError
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; private set; }

        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ErrorType { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }


        public ApiError() { }

        public ApiError(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            ErrorType = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message) : this(statusCode, statusDescription)
        {
            Message = message;
        }
    }
}