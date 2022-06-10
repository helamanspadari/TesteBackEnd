using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Service
{
    public interface IServiceDelete<T> where T : BaseEntity
    {
        void Delete(int id);
        void DeleteManyToMany(object id, object id2);
    }
}
