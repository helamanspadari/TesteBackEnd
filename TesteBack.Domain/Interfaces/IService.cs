using TesteBack.Domain.Entities;
using TesteBack.Domain.Interfaces.Service;

namespace TesteBack.Domain.Interfaces
{
    public interface IService<T> :
        IServicePost<T>,
        IServicePut<T>,
        IServiceGet<T>,
        IServiceDelete<T> where T : BaseEntity
    { }
}
