using Shared.Core.API.ViewModel;
using Shared.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Core.Application
{
    public interface IApplicationService<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<TView> GetAsync<TView>(Guid id) where TView : IViewModel;
        Task<TView> GetAsync<TView>() where TView : IViewModel;
        Task<IEnumerable<T>> GetAsync();
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}
