using AuthorizationApp.Data;
using AuthorizationApp.Models;
using AuthorizationApp.Utilities.Interceptors;
using AuthorizationApp.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Service
{
    public class UserManager : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;

        public UserManager(IGenericRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [SecuredOperation("admin")]
        public IDataResult<List<User>> GetAll()
        {
            return new DataResult<List<User>>(_genericRepository.GetAll(),true);

        }
    }
}
