using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.ApiErrors;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Net;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides abstraction for the most common controller operations.
    /// </summary>
    public abstract class CustomApiController : ControllerBase
    {
        private readonly ILogger _logger;


        public CustomApiController() { }

        public CustomApiController(ILogger logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Logs message and exception if passed that caused <see cref="HttpStatusCode.NotFound"/> error. Creates and returns <see cref="NotFoundObjectResult"/>, which describes the error.
        /// </summary>
        /// <param name="exception">An exception that causes NotFoundError.</param>
        /// <param name="notFoundObject">An object that processing causes NotFoundError.</param>
        /// <returns>A <see cref="NotFoundObjectResult"/> described en NotFoundError.</returns>
        protected virtual NotFoundObjectResult OnNotFoundError(string errorMessage, Exception exception = null)
        {
            if (exception is null)
            {
                _logger.LogWarning($"{errorMessage}");
            }
            else
            {
                _logger.LogWarning(exception, $"{exception.GetType().Name} - {errorMessage}");
            }

            var error = new NotFoundError(errorMessage);
            var errorResponse = new ResponseWrapper(error);
            return NotFound(errorResponse);
        }

        /// <summary>
        /// Logs message and exception if passed that caused <see cref="InvalidParameterError"/> and as a result, <see cref="HttpStatusCode.BadRequest"/> error. 
        /// Creates and returns <see cref="BadRequestObjectResult"/>, which describes the error.
        /// </summary>
        /// <param name="exception">An exception that caused <see cref="HttpStatusCode.BadRequest"/>.</param>
        /// <param name="notFoundObject">An object that processing caused <see cref="HttpStatusCode.BadRequest"/>.</param>
        /// <returns>A <see cref="BadRequestObjectResult"/> described en <see cref="HttpStatusCode.BadRequest"/>.</returns>
        protected virtual BadRequestObjectResult OnInvalidParameterError(string errorMessage, Exception exception = null)
        {
            if (exception is null)
            {
                _logger.LogWarning($"{errorMessage}");
            }
            else
            {
                _logger.LogWarning(exception, $"{exception.GetType().Name} - {errorMessage}");
            }

            var error = new InvalidParameterError(errorMessage);
            var errorResponse = new ResponseWrapper(error);
            return BadRequest(errorResponse);
        }

        /// <summary>
        /// Logs message and exception if passed that caused <see cref="MismatchParameterError"/> and as a result, <see cref="HttpStatusCode.BadRequest"/> error. 
        /// Creates and returns <see cref="BadRequestObjectResult"/>, which describes the error.
        /// </summary>
        /// <param name="exception">An exception that caused <see cref="HttpStatusCode.BadRequest"/>.</param>
        /// <param name="notFoundObject">An object that processing caused <see cref="HttpStatusCode.BadRequest"/>.</param>
        /// <returns>A <see cref="BadRequestObjectResult"/> described en <see cref="HttpStatusCode.BadRequest"/>.</returns>
        protected virtual BadRequestObjectResult OnMismatchParameterError(string errorMessage, Exception exception = null)
        {
            if (exception is null)
            {
                _logger.LogWarning($"{errorMessage}");
            }
            else
            {
                _logger.LogWarning(exception, $"{exception.GetType().Name} - {errorMessage}");
            }

            var error = new MismatchParameterError(errorMessage);
            var errorResponse = new ResponseWrapper(error);
            return BadRequest(errorResponse);
        }

        /// <summary>
        /// Logs <see cref="InternalDbServiceException"/> error occurrence.
        /// </summary>
        /// <param name="errorMessage">Log message.</param>
        /// <param name="dbServiceType">A type of database service.</param>
        /// <param name="exception">The <see cref="InternalDbServiceException"/>.</param>
        protected virtual void LogInternalDbServiceException(InternalDbServiceException exception, Type dbServiceType = null, string errorMessage = null)
        {
            string fullMessage = dbServiceType is null ? $"{exception.GetType().Name} - An error occurred while database operation was proccessing. {exception.Message}" :
                $"{exception.GetType().Name} - An error occurred at '{dbServiceType.Name}' while database operation was proccessing. {exception.Message}";
            _logger.LogError(exception, fullMessage);
        }

        /// <summary>
        /// Logs unexpected <see cref="Exception"/> error occurrence.
        /// </summary>
        /// <param name="errorMessage">Log message.</param>
        /// <param name="exception">The <see cref="Exception"/>.</param>
        protected virtual void LogUnexpectedException(Exception exception, string errorMessage = null)
        {
            _logger.LogError(exception, $"{exception.GetType().Name} - Unexpected internal error occured. {exception.Message}");
        }
    }
}