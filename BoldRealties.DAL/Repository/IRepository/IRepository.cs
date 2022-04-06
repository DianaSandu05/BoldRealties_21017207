using System.Linq.Expressions;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IRepository<E> where E : class
    {
        //P - Property
        IEnumerable<E> GetAll(
            Expression<Func<E, bool>> filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null,
            string includeProperties = null
            );

        E GetFirstOrDefault(
            Expression<Func<E, bool>> filter = null,
            string includeProperties = null, bool tracked = true
            );

        void Add(E entity);
        void Remove(E entity);
        void RemoveRange(IEnumerable<E> entity);
    }
}
