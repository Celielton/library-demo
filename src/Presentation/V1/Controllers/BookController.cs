using AutoMapper;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.API.Controller;
using Shared.Core.Application;
using Shared.Core.Infrastructure.UnitOfWork;

namespace library_api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BookController : ApiControllerBase<Book, BookCommand>
    {
        public BookController(IApplicationService<Book> applicationService, IUnitOfWork unitOfWork, IMapper mapper)
                : base(applicationService, unitOfWork, mapper)
        {

        }
    }
}
