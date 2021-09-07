using AuthorizationApp.Data.Dtos;
using AuthorizationApp.Models;
using AuthorizationApp.Utilities.Results;
using AuthorizationApp.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Service
{
    public interface IAuthService
    {
        IDataResult<User>Register(RegisterDto registerDto, string password);
        IDataResult<User> Login(LoginDto loginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByMail(string mail);
    }
}
