using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDCWebApp.ApiErrors;

namespace SDCWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        // These messages will only be used for generic errors, when no specific message is available.
        private const string InternalServerErrorMessage = "An unexpected internal error occurred. Will be logged.";
        private const string UnauthorizedErrorMessage = "No valid access token was passed.";
        private const string ForbiddenErrorMessage = "The access token passed does not have sufficient permissions.";


        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            string genericErrorMessage = null;

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

            ApiError apiError = new ApiError(code, parsedCode.ToString(), genericErrorMessage);
            Response.ContentType = "application/problem+json";
           //Response.Headers.Add("Content - Language", "en");

            return new ObjectResult(new { error = apiError });
        }      
    }
}