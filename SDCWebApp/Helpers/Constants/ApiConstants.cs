﻿namespace SDCWebApp.Helpers.Constants
{
    /// <summary>
    /// Contains constants strings used accros entire application.
    /// </summary>
    public static class ApiConstants
    {
        #region CustomJwtClaimNames

        public const string RoleClaim = "role";

        #endregion


        #region Api roles

        public const string AdministratorRole = "administrator";
        public const string ModeratorRole = "moderator";

        #endregion


        #region Api authorization policy

        public const string ApiUserPolicy = "ApiUser";
        public const string ApiAdminPolicy = "ApiAdmin";

        #endregion


        #region Database settings

        public const string DefaultConnectionString = "DefaultConnection";
        public const string DbPassword = "Db:DbPassword";
        public const string DbUserId = "Db:DbUserId";

        #endregion


        #region Azure App Configuration

        public const string AzureAppConfigEndpoint = "https://sdc-app-config.azconfig.io";

        #endregion


        #region CORS policy

        public const string DefaultCorsPolicy = "EnableCors";

        #endregion


        #region Api identity settings

        public const string AdminSeedPassword = "IdentitySeed:AdminPassword";
        public const string ModeratorSeedPassword = "IdentitySeed:ModeratorPassword";

        #endregion

    }
}
