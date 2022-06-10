using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Repository
{
    public interface IRepositoryRemove<T> where T : BaseEntity
    {
        void Remove(int id);
        void RemoveManyToMany(object id, object id2);
    }
}
