using EventManagement.Domain.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        T GetById(int id, bool isDeleted = false);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll(bool isDeleted = false);
        IEnumerable<T> Filter(Expression<Func<T,bool>> filter, bool isDeleted = false);
    }
}
