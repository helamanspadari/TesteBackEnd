using FluentValidation;
using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Service
{
    public interface IServicePut<T> where T : BaseEntity
    {
        T Put<V>(T obj) where V : AbstractValidator<T>;
    }
}
