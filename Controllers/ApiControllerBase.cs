using AutoMapper;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using library_api.Models;
using library_api.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace library_api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[Controller]")]    
    [Produces("application/json")]
    public class ApiControllerBase<T, TEntity> : ControllerBase where T : Entity where TEntity : ICommand
    {
        private readonly IRepository<T> _entityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApiControllerBase(IRepository<T> entityRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _entityRepository.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _entityRepository.FindAsync(id);
            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _entityRepository.FindAsync(id);
            if (entity == null) return NotFound();

            _entityRepository.Remove(entity);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TEntity cmd)
        {
            var entity = _mapper.Map<T>(cmd);
            _entityRepository.Add(entity);
            await _unitOfWork.CommitAsync();

            return StatusCode((int)HttpStatusCode.Created, new { entity.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TEntity cmd)
        {
            await _unitOfWork.BeginTransactionAsync();
            var entity = await _entityRepository.FindAsync(id);
            if (entity == null) return NotFound();

            entity = _mapper.Map(cmd, entity);
            _entityRepository.Update(entity);

            await _unitOfWork.CommitTransactionAsync();

            return Ok();
        }

        [HttpPatch("id")]
        public async Task<IActionResult> Patch(Guid id, TEntity cmd)
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = await _entityRepository.FindAsync(id);
            if (entity == null) return NotFound();

            foreach (var property in cmd.GetType().GetProperties())
            {
                var value = property.GetValue(cmd);
                if (value != null)
                    entity.GetType().GetProperty(property.Name).SetValue(entity, value);
            }

            _entityRepository.Update(entity);

            await _unitOfWork.CommitTransactionAsync();

            return Ok();
        }

    }
}
