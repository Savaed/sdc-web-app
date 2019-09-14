using SDCWebApp.ApiErrors;

namespace SDCWebApp.Models.Dtos
{
    /// <summary>
    /// Encapsulates common server response field such as Data and Error.
    /// </summary>
    public class ResponseWrapper
    {
        public object Data { get; set; }
        public ApiError Error { get; set; }


        public ResponseWrapper()
        {
            Data = new object();
            Error = new ApiError();
        }

        public ResponseWrapper(object data, ApiError error)
        {
            Data = data;
            Error = error;
        }

        public ResponseWrapper(object data)
        {
            Data = data;
            Error = new ApiError();
        }

        public ResponseWrapper(ApiError error)
        {
            Data = new object();
            Error = error;
        }
    }
}
