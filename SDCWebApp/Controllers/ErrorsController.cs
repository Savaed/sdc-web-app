using Microsoft.AspNetCore.Mvc;
using System.Net;

using SDCWebApp.ApiErrors;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Custom error responses with default, generic messages.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ErrorsController : CustomApiController
    {
        // These messages will only be used for generic errors, when no specific message is available.
        private const string InternalServerErrorMessage = "An unexpected internal error occurred. Will be logged.";
        private const string UnauthorizedErrorMessage = "No valid access token was passed.";
        private const string ForbiddenErrorMessage = "The access token passed does not have sufficient permissions.";

        /// <summary>
        /// Generate custom error response.
        /// </summary>
        /// <param name="code">The error <see cref="HttpStatusCode"/>.</param>
        /// <returns><see cref="ObjectResult"/> error response.</returns>
        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            string genericErrorMessage = "";

            switch (code)
            {
                case 401:
                    genericErrorMessage = UnauthorizedErrorMessage;
                    break;
                case 403:
                    genericErrorMessage = ForbiddenErrorMessage;
                    break;
                case 500:
                    genericErrorMessage = InternalServerErrorMessage;
                    break;
                default:
                    break;
            }

            // Set the response header for error according to https://tools.ietf.org/html/rfc7807#section-6
            Response.ContentType = "application/problem+json";
            ApiError apiError = new ApiError(code, parsedCode.ToString(), genericErrorMessage);

            return new ObjectResult(new { error = apiError });
        }
    }
}