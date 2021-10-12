using library_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace library_api.Infrastructure.Repository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> FindByNameAsync(string keyWord);
    }
}
