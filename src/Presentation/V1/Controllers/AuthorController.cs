using AutoMapper;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.API.Controller;
using Shared.Core.Application;
using Shared.Core.Infrastructure.UnitOfWork;

namespace library_api.V1.Controllers
{
    [ApiVersion("1.0")]
    public class AuthorController : ApiControllerBase<Author, AuthorCommand>
    {
        public AuthorController(IApplicationService<Author> applicationService, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(applicationService, unitOfWork, mapper)
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
