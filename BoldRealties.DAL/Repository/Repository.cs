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

        public IEnumerable<E> GetAll()
        {
            IQueryable<E> query = dbset;
            return query.ToList();
        }

        public E GetFirstOrDefault(Expression<Func<E, bool>> filter)
        {
            IQueryable<E> query = dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
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
