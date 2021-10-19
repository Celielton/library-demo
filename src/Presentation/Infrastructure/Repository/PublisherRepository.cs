using library_api.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_api.Infrastructure.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Publisher>> FindByNameAsync(string keyWord)
        {
            return await DbSet.Where(n => EF.Functions.ILike(n.Name, $"%{keyWord}%")).ToListAsync();
        }
    }
}
