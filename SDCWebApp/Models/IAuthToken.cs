using System;

namespace SDCWebApp.Models
{
    public interface IAuthToken
    {
        string Token { get; set; }
        DateTime ExpiryIn { get; set; }
    }
}
