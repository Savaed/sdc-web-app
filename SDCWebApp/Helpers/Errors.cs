using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Helpers
{
    public static class Errors
    {
        #region ErrorsCodes

        public const string NotFoundCode = "ElementNotFound";
        public const string InternalServerErrorCode = "InternalServerError";

        #endregion

        #region ErrorsMessages

        public const string NotFoundMessage = "Cannot found specified element. Please check parameters in request.";
        public const string InternalServerErrorGeneralMessage = "Server encountered uneexpected error.";
        public const string InternalServerErrorDbNotExistMessage = "Server encountered uneexpected error. Databse not exist.";
        public const string InternalServerErrorResourceNotExistMessage = "Server encountered uneexpected error. Resource in databse which is requested not exist.";


        #endregion
    }
}
