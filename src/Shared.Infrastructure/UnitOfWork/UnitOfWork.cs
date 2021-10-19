using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Core.Domain.Interfaces;
using Shared.Core.Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext Context;
        private static IDbContextTransaction transaction;
        private bool disposed;


        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await Context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            foreach (var change in Context.ChangeTracker.Entries())
            {
                Console.WriteLine($"\n\nState: {change.State}\nEntity: {change.Entity}\n\n");
                if (change.Entity.GetType().Name == typeof(IMutableEntity).Name && change.State == EntityState.Modified)
                {
                    var entity = ((IMutableEntity)change.Entity);
                    entity.ModifiedDate = DateTime.UtcNow;
                    entity.Version += 1;
                }
            }

            return await Context.SaveChangesAsync();
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing && Context != null)
            { 
                Context.Dispose();
            }

            disposed = true;
        }
    }
}
