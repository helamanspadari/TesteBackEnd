using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Repository
{
    public interface IRepositoryUpdate<T> where T : BaseEntity
    {
        void Update(T obj);
    }
}
