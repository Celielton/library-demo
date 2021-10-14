using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace library_api.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDBContext _context;
        private static IDbContextTransaction transaction;

        public UnitOfWork(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            foreach (var change in _context.ChangeTracker.Entries())
            {
                Console.WriteLine($"\n\nState: {change.State}\nEntity: {change.Entity}\n\n");
            }

            return await _context.SaveChangesAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await transaction.RollbackAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await CommitAsync();
            
            Console.WriteLine("Transaction will commit");

            await transaction.CommitAsync();

            Console.WriteLine("Transaction committed successfully");

        }
    }
}
