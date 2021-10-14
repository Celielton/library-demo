using library_api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace library_api.Infrastructure.Repository
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<T> FindAsync(Guid id);
        Task<IEnumerable<T>> ListAllAsync();
    }
}
