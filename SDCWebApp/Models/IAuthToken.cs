using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models
{
    public interface IAuthToken
    {
        string Token { get; set; }
        int ExpiryIn { get; set; }
    }
}
