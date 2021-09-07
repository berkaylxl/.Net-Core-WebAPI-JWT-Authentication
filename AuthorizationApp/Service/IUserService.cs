using AuthorizationApp.Models;
using AuthorizationApp.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Service
{
     public interface IUserService
    {
        IDataResult<List<User>> GetAll();
    }
}
