using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Entities;
using Shared.Core.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity 
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> DbSet;
        private bool disposed;

        public Repository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if(disposing && _context != null)
            {
                _context.Dispose();
            }

            disposed = true;
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
