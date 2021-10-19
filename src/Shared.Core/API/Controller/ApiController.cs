using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.API.Commands;
using Shared.Core.Application;
using Shared.Core.Domain.Entities;
using Shared.Core.Infrastructure.Repository;
using Shared.Core.Infrastructure.UnitOfWork;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shared.Core.API.Controller
{
    [ApiController]
    [Route("v{version:apiVersion}/[Controller]")]
    [Produces("application/json")]
    public class ApiControllerBase<T, TEntity> : ControllerBase where T : Entity where TEntity : ICommand
    {
        private readonly IApplicationService<T> _applicationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApiControllerBase(IApplicationService<T> applicationService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _applicationService  = applicationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _applicationService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _applicationService.GetAsync(id);
            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _applicationService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TEntity cmd)
        {
            var entity = _mapper.Map<T>(cmd);
            await _applicationService.CreateAsync(entity);


            return StatusCode((int)HttpStatusCode.Created, new { entity.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TEntity cmd)
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = await _applicationService.GetAsync(id);
            if (entity == null) return NotFound();

            entity = _mapper.Map(cmd, entity);
            await _applicationService.UpdateAsync(entity);

            await _unitOfWork.CommitTransactionAsync();

            return Ok();
        }

        [HttpPatch("id")]
        public async Task<IActionResult> Patch(Guid id, TEntity cmd)
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = await _applicationService.GetAsync(id);
            if (entity == null) return NotFound();

            foreach (var property in cmd.GetType().GetProperties())
            {
                var value = property.GetValue(cmd);
                if (value != null)
                    entity.GetType().GetProperty(property.Name).SetValue(entity, value);
            }

            await _applicationService.UpdateAsync(entity);

            await _unitOfWork.CommitTransactionAsync();

            return Ok();
        }

    }
}
