using AuthorizationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthorizationApp.Data
{
    public class EfRepositoryBase<T> :IGenericRepository<T> where T:class,IEntity,new()
    {
        public void Add(T entity)
        {
            using (DataContext context = new DataContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            using (DataContext context = new DataContext())
            {
                return context.Set<T>().FirstOrDefault(filter);
            }
        }
        public List<T> GetAll()
        {
            using (DataContext context = new DataContext())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
