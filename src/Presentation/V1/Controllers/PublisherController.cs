using AutoMapper;
using library_api.Infrastructure.Repository;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.API.Controller;
using Shared.Core.Application;
using Shared.Core.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace library_api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class PublisherController : ApiControllerBase<Publisher, PublisherCommand>
    {
        private readonly IApplicationService<Publisher> _applicationService;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherController(IApplicationService<Publisher> applicationService, IUnitOfWork unitOfWork, IMapper mapper, IPublisherRepository publisherRepository)
            : base(applicationService, unitOfWork, mapper)
        {
            _applicationService = applicationService;
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string keyword)
        {
            var result = await _publisherRepository.FindByNameAsync(keyword);
            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
