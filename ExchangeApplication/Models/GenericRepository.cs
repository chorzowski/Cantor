using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ExchangeApplication.Models
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext db = null;

        IDbSet<T> _objectSet;
        public GenericRepository(ApplicationDbContext _db)
        {
            db = _db;
            _objectSet = db.Set<T>();
        }
        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }
        public T GetDetail(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.First(predicate);
        }

        public virtual T FindSingle(int id)
        {
            return db.Set<T>().Find(id);
        }
        public virtual T FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = db.Set<T>();
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefault();
        }




        //public T GetDetail(Func<T, bool> predicate)
        //{
        //    return _objectSet.First(predicate);
        //    // throw new NotImplementedException();
        //}

        public IEnumerable<T> GetOverview(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet.AsEnumerable();
        }

        //public IEnumerable<T> GetOverview(Func<T, bool> predicate = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

