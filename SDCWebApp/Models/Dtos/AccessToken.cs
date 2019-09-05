using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class AccessToken : IAuthToken
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }


        public AccessToken(string token, int expiryIn)
        {
            Token = token;
            ExpiryIn = expiryIn;
        }
    }
}
