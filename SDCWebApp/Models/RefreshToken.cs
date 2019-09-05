using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models
{
    public class RefreshToken : BasicEntity, IAuthToken
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }
    }
}
