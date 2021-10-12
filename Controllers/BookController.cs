﻿using AutoMapper;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using library_api.Models;
using library_api.Models.Commands;

namespace library_api.Controllers
{
    public class BookController : ApiControllerBase<Book, BookCommand>
    {
        public BookController(IRepository<Book> repository, IUnitOfWork unitOfWork, IMapper mapper)
                : base(repository, unitOfWork, mapper)
        {

        }
    }
}
