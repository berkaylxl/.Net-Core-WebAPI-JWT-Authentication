
using AuthorizationApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationApp.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenoptions;
        private DateTime _accesTokenExpritaion;

        public JwtHelper(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            _tokenoptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accesTokenExpritaion = DateTime.Now.AddMinutes(_tokenoptions.AccesTokenExpiration);
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenoptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                 issuer: _tokenoptions.Issuer,
                 audience: _tokenoptions.Audience,
                 expires: _accesTokenExpritaion,
                 notBefore: DateTime.UtcNow,
                 claims: SetClaims(user, operationClaims),
                 signingCredentials: signingCredentials);
             var jwtTokenHandler = new JwtSecurityTokenHandler();
             var token = jwtTokenHandler.WriteToken(jwt);

            return new AccessToken {
                Token = token,
                Expiration = _accesTokenExpritaion
                
            };
        }
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));;
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            var roles = operationClaims.Select(c => c.Name);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
