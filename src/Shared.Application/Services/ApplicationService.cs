using AutoMapper;
using Shared.Core.API.ViewModel;
using Shared.Core.Application;
using Shared.Core.Domain.Entities;
using Shared.Core.Infrastructure.Repository;
using Shared.Core.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Application.Services
{
    public class ApplicationService<T> : IApplicationService<T> where T : Entity 
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }


        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<TView> GetAsync<TView>(Guid id) where TView : IViewModel
        {
            var entity = await _repository.GetAsync(id);
            return _mapper.Map<TView>(entity);
        }

        public async Task<TView> GetAsync<TView>() where TView : IViewModel
        {
            var entity = await _repository.GetAsync();
            return _mapper.Map<TView>(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                throw new NotImplementedException(); //NotFoundException

            await _unitOfWork.BeginTransactionAsync();

            _repository.Remove(entity);

            await _unitOfWork.CommitAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            var oldEntity = await _repository.GetAsync(entity.Id);
            if (oldEntity == null)
                throw new NotImplementedException(); //NotFoundException

            await _unitOfWork.BeginTransactionAsync();

            _repository.Update(entity);

            await _unitOfWork.CommitAsync();
        }
    }
}
