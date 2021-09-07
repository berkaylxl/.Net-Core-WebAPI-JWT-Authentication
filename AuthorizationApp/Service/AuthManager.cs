using AuthorizationApp.Data;
using AuthorizationApp.Data.Dtos;
using AuthorizationApp.Models;
using AuthorizationApp.Utilities.Results;
using AuthorizationApp.Utilities.Security.Hashing;
using AuthorizationApp.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Service
{
    public class AuthManager : IAuthService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private ITokenHelper _tokenHelper;

        public AuthManager(IGenericRepository<User> genericRepository, ITokenHelper tokenHelper)
        {
            _genericRepository = genericRepository;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new DataResult<AccessToken>(accessToken, true,"Token Oluşturuldu");
        }

        public IDataResult<List<User>> GetAll()
        {
            return new DataResult<List<User>>(_genericRepository.GetAll(), true);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            var result = _genericRepository.Get(x => x.Email == mail);
            if (result==null)
            {
                return new DataResult<User>(result, true);
            }
            return new DataResult<User>(null, false);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            using (var context = new DataContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userClaim in context.UserClaims
                             on operationClaim.Id equals userClaim.OperationClaimId
                             where userClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return new DataResult<List<OperationClaim>>(result.ToList(), true);
            }
        }

        public IDataResult<User> Login(LoginDto loginDto)
        {
            var userToCheck = _genericRepository.Get(x => x.Email == loginDto.Email);
            if (userToCheck == null)
            {
                return new DataResult<User>(null, false);
            }
            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new DataResult<User>(null, false,"Kullanıcı Bulunamadı");
            }
            return new DataResult<User>(userToCheck, true,"Giriş Başarılı");
        }

        public IDataResult<User> Register(RegisterDto registerDto, string password)
        {
            byte[] _passwordHash, _passwordSalt;
            HashingHelper.CreatePasswordHash(password, out _passwordHash, out _passwordSalt);
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Name,
                PasswordHash = _passwordHash,
                PasswordSalt = _passwordSalt,
                Status = true
            };
            _genericRepository.Add(user);
            return new DataResult<User>(user, true,"Kayıt İşlemi Başarılı");
        }
    }
}
