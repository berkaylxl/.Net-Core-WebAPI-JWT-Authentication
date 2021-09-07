using AuthorizationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthorizationApp.Data
{
    public interface IGenericRepository<T> where T :class,IEntity
    {
        void Add(T entity);
        List<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
    }
}
