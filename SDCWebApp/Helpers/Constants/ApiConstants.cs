namespace SDCWebApp.Helpers.Constants
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

        public const string AdministratorRole = "admin";
        public const string ModeratorRole = "mod";

        #endregion


        #region Api authorization policy

        public const string ApiUserPolicy = "ApiUser";
        public const string ApiAdminPolicy = "ApiAdmin";

        #endregion


        #region Db conections

        public const string DefaultConnectionString = "DefaultConnection";

        #endregion


        #region CORS policy

        public const string DefaultCorsPolicy = "EnableCors";

        #endregion

    }
}
