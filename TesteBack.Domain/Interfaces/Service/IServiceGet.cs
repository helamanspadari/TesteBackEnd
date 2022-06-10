using System;
using System.Collections.Generic;
using TesteBack.Domain.Entities;

namespace TesteBack.Domain.Interfaces.Service
{
    public interface IServiceGet<T> where T : BaseEntity
    {
        T Get(int id);

        IList<T> Get();

        IEnumerable<T> Get(Func<T, bool> query);

        bool Exist(Func<T, bool> query);
    }
}
