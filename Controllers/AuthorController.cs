using AutoMapper;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using library_api.Models;
using library_api.Models.Commands;

namespace library_api.Controllers
{
    public class AuthorController : ApiControllerBase<Author, AuthorCommand>
    {
        public AuthorController(IRepository<Author> entityRepository, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityRepository, unitOfWork, mapper)
        {
        }      
    }

    //C# 9 feature
    record AuthorViewModel(string FirstName, string LastName);
    public class Test
    {
        public string Name { get; init; /*init works only in constructor phase*/ }
    }
}
