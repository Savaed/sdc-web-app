using Newtonsoft.Json;

namespace SDCWebApp.ApiErrors
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        [JsonProperty("type")]
        public string ErrorType { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }


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