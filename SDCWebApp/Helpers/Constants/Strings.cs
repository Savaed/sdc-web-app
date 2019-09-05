using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Helpers.Constants
{
    /// <summary>
    /// Contains constants strings used accros entire application.
    /// </summary>
    public static class Strings
    {
        #region CustomJwtClaimNames

        public const string RoleClaimName = "role";

        #endregion


        #region Api roles

        public const string AdministratorRoleName = "admin";
        public const string ModeratorRoleName = "mod";

        #endregion


        #region Api authorization policy

        public const string ApiUserPolicyName = "ApiUser";

        #endregion


        #region Db conections

        public const string DefaultConnectionStringName = "DefaultConnection";

        #endregion


        #region CORS policy

        public const string DefaultCorsPolicyName = "EnableCors";

        #endregion

    }
}
