using TesteBack.Domain.Entities;
using TesteBack.Domain.Interfaces.Repository;

namespace TesteBack.Domain.Interfaces
{
    public interface IRepository<T> :
            IRepositoryInsert<T>,
            IRepositorySelect<T>,
            IRepositoryUpdate<T>,
            IRepositoryRemove<T> where T : BaseEntity
    { }
}
