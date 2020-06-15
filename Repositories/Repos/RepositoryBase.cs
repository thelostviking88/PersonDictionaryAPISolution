using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Repos
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly PersonDBContext _dBContext;
        private readonly DbSet<T> _dBSet;

        public RepositoryBase(PersonDBContext context)
        {
            _dBContext = context;
            _dBSet = _dBContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dBSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dBSet.RemoveRange(entity);
        }

        public IQueryable<T> GetAll()
        {
           return _dBSet.AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dBSet.Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _dBSet.Attach(entity);
            _dBContext.Entry(entity).State = EntityState.Modified;
        }
      
    }
}
