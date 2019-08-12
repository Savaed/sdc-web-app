namespace SDCWebApp.Helpers
{
    /// <summary>
    /// DTO class for retriving Jason Web Token settings from settings file.
    /// </summary>
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string ExpiryTime { get; set; }
        public string Secret { get; set; }
    }
}
