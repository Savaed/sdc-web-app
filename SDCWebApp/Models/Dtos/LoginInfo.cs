namespace SDCWebApp.Models.Dtos
{
    public class LoginInfo
    {
        public UserDto User { get; set; }
        public AccessToken AccessToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
