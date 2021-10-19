using System;
using System.Threading.Tasks;

namespace Shared.Core.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    {
        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}
