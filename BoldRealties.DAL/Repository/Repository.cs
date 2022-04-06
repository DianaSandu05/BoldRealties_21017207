using BoldRealties.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BoldRealties.DAL.Repository
{
    public class Repository<E> : IRepository<E> where E : class
    {
        private readonly BoldRealties_dbContext _db;
        internal DbSet<E> dbset;
        public Repository(BoldRealties_dbContext db)
        {
            _db = db;
            this.dbset = _db.Set<E>();
        }
        public void Add(E entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<E> GetAll(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = null)
        {
            IQueryable<E> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public E GetFirstOrDefault(Expression<Func<E, bool>> filter = null, string includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<E> query = dbset;

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<E> query = dbset.AsNoTracking();

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }

        }
        public void Remove(E entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<E> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
