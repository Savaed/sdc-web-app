using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SDCWebApp.ApiErrors;
using System.Net;

namespace SDCWebApp.Helpers
{
    public class CustomValidationProblemDetails
    {
        public ApiError Error { get; set; }


        public CustomValidationProblemDetails(ActionContext context)
        {
            Error = ConstructValidationError(context);
        }


        private ApiError ConstructValidationError(ActionContext context)
        {
            string errorMessage = "Malformed request.";

            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    // Only first error will be returned to the client.
                    errorMessage = GetErrorMessage(errors[0]);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.HttpContext.Response.ContentType = "application/problem+json";
                }
            }

            return new ApiError(400, HttpStatusCode.BadRequest.ToString() + "Error", errorMessage);
        }


        private string GetErrorMessage(ModelError error) => string.IsNullOrEmpty(error.ErrorMessage) ? "Malformed request." : error.ErrorMessage;
    }
}
