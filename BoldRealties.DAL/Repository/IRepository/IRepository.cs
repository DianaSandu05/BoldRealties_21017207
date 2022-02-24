using System.Linq.Expressions;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IRepository<E> where E : class
    {
        //P - Property
        E GetFirstOrDefault(Expression<Func<E, bool>> filter);
        IEnumerable<E> GetAll();
        void Add(E entity);
        void Remove(E entity);
        void RemoveRange(IEnumerable<E> entity);
    }
}
