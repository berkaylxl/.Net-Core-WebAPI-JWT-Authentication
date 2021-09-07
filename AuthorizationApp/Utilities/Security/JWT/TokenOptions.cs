using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string  Audience { get; set; }
        public string  Issuer { get; set; }
        public int AccesTokenExpiration { get; set; }
        public string  SecurityKey { get; set; }
    }
}
