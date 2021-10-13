using AutoMapper;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace library_api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class PublisherController : ApiControllerBase<Publisher, PublisherCommand>
    {
        private readonly IPublisherRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherController(IPublisherRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
            :base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
       
        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string keyword)
        {
            var result = await _repository.FindByNameAsync(keyword);
            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
