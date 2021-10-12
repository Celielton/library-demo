using library_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace library_api.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly LibraryDBContext _context;
        protected readonly DbSet<T> DbSet;

        public Repository(LibraryDBContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
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
    }
}
