using AutoMapper;
using library_api.Controllers;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;

namespace library_api.V1.Controllers
{
    [ApiVersion("1.0")]
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
