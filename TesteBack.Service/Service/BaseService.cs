using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBack.Domain.Entities;
using TesteBack.Domain.Interfaces;

namespace TesteBack.Service.Service
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        static IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Insert(obj);
            return obj;
        }

        public async Task PostListAsync(List<T> obj)
        {
            await _repository.InsertListAsync(obj);
        }

        public async Task<T> PostAsync<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            await _repository.InsertAsync(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Update(obj);
            return obj;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            _repository.Remove(id);
        }

        public void DeleteManyToMany(object id, object id2)
        {
            _repository.RemoveManyToMany(id, id2);
        }

        public IList<T> Get() => _repository.SelectAll();

        public async Task<IList<T>> GetAsync() => await _repository.SelectAllAsync();

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return _repository.Select(id);
        }

        public async Task<T> GetAsync(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return await _repository.SelectAsync(id);
        }

        public IEnumerable<T> Get(Func<T, bool> query)
        {
            if (query == null)
                throw new ArgumentException("The query can't be null.");

            return _repository.SelectBy(query);
        }

        public async Task<IList<T>> GetAsync(Func<T, bool> query)
        {
            if (query == null)
                throw new ArgumentException("The query can't be null.");

            return await _repository.SelectByAsync(query);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        public bool Exist(Func<T, bool> query)
        {
            return _repository.Exist(query);
        }

        public static bool ExistStatic(Func<T, bool> query)
        {
            return _repository.Exist(query);
        }
    }
}
