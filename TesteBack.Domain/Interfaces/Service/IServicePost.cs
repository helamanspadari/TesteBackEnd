using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Service
{
    public interface IServicePost<T> where T : BaseEntity
    {
        T Post<V>(T obj) where V : AbstractValidator<T>;
        Task<T> PostAsync<V>(T obj) where V : AbstractValidator<T>;
        Task PostListAsync(List<T> obj);
    }
}
