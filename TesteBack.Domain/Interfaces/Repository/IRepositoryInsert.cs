using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Repository
{
    public interface IRepositoryInsert<T> where T : BaseEntity
    {
        void Insert(T obj);

        Task InsertListAsync(List<T> obj);

        Task InsertAsync(T obj);
    }
}
