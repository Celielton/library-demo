using System.Threading.Tasks;

namespace library_api.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}
