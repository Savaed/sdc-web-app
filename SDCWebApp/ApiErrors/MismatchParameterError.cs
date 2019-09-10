﻿namespace SDCWebApp.ApiErrors
{
    public class MismatchParameterError : ApiError
    {
        public MismatchParameterError() : base(400, "MismatchParameterError") { }

        public MismatchParameterError(string message) : base(400, "MismatchParameterError", message) { }

    }
}
