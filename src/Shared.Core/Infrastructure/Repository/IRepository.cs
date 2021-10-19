using Shared.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Core.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync();
    }
}
