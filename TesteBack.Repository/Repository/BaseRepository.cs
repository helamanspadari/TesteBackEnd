using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBack.Domain.Entities;
using TesteBack.Domain.Interfaces;
using TesteBack.Repository.Context;

namespace TesteBack.Repository.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método génerico para realizar insert
        /// </summary>
        /// <param name="obj">entidade a ser salvo</param>
        public void Insert(T obj)
        {
            try
            {
                _context.Set<T>().Add(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is T)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
        }

        public async Task InsertListAsync(List<T> obj)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is T)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
        }

        public async Task InsertAsync(T obj)
        {
            try
            {
                _context.Set<T>().Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método génerico para realizar update
        /// </summary>
        /// <param name="obj">entidade a ser alterado</param>
        public void Update(T obj)
        {
            try
            {
                _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is T)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
        }

        /// <summary>
        /// método génerico para realizar delete
        /// </summary>
        /// <param name="id">id da linha a ser excluida</param>
        public void Remove(int id)
        {
            _context.Set<T>().Remove(Select(id));
            _context.SaveChanges();
        }

        public void RemoveManyToMany(object id, object id2)
        {
            _context.Set<T>().Remove(SelectManyToMany(id, id2));
            _context.SaveChanges();
        }

        /// <summary>
        /// método génerico para listar as informações de acordo com a entidade informada no constructor
        /// </summary>
        /// <returns>retorna uma lista de acorda com a entidade informada no servidor</returns>
        public IList<T> SelectAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IList<T>> SelectAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// método génerico para obter as informações de acordo com a entidade informada no constructor
        /// </summary>
        /// <param name="id">id da linha a consultar</param>
        /// <returns>retorna um objeto de acorda com a entidade informada no servidor</returns>
        public T Select(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T SelectManyToMany(object id, object id2)
        {
            return _context.Set<T>().Find(id, id2);
        }

        public async Task<T> SelectAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// método génerico para obter as informações de acordo com a entidade informada no constructor
        /// de acordo com uma query informada no parametro
        /// </summary>
        /// <param name="query">query para consulta na tabela</param>
        /// <returns>retorna uma lista ou objeto de acorda com a entidade informada no servidor</returns>
        public List<T> SelectBy(Func<T, bool> query)
        {
            return _context.Set<T>().Where(query).ToList();
        }

        public async Task<IList<T>> SelectByAsync(Func<T, bool> query)
        {
            var lista = await _context.Set<T>().ToListAsync();
            return lista.Where(query).ToList();
        }

        /// <summary>
        /// Método responsavel por validar se existe uma informação na tabela 
        /// </summary>
        /// <param name="query">query para consulta na tabela</param>
        /// <returns>Retorna true caso exista na tabela</returns>
        public bool Exist(Func<T, bool> query)
        {
            return _context.Set<T>().Any(query);
        }
    }

}
