using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationApp.Utilities.Interceptors
{
    public class SecuredOperation:MethodInterception
    {
        private string [] _roles;
        HttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');             
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User
                .FindAll(ClaimTypes.Role).Select(x=>x.Value).ToList();

            foreach (var role  in _roles)
            {

                if (roleClaims.Contains(role))
                {
                    return;
                }

            }
            throw new Exception("Yetkiniz Yoktur");

        }
    }
}
