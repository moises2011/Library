using AutoMapper;
using Library.Core.Dtos;
using Library.Core.Interfaces;
using Library.Data.Helpers;
using Library.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class Service<TId, TEntity, TEntityDto> : IService<TId, TEntity, TEntityDto>
        where TId : struct
        where TEntityDto : EntityBase<TId>
        where TEntity : Data.Entities.EntityBase<TId>
    {
        private readonly IERepository<TId, TEntity> repository;
        private readonly ILoggerHelper loggerHelper;

        public Service(IERepository<TId, TEntity> repository, ILoggerHelper loggerHelper)
        {
            this.repository = repository;
            this.loggerHelper = loggerHelper;
        }

        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            loggerHelper.LogInfo(GetType().FullName, "Entidad consultada");
            return from book in await repository.GetAllAsync()
                   select Mapper.Map<TEntityDto>(book);
        }
        public async Task<TEntityDto> FindByIdAsync(TId id)
        {
            TEntity book = await repository.FindByIdAsync(id);
            return book != null ? Mapper.Map<TEntityDto>(book) : null;
        }
        public async Task AddAsync(TEntityDto entity)
        {
            await repository.AddAsync(Mapper.Map<TEntity>(entity));
        }
        public async Task DeleteAsync(TEntityDto entity)
        {
            await repository.DeleteAsync(Mapper.Map<TEntity>(entity));
        }
        public async Task DeleteAsync(TId entityId)
        {
            TEntity entity = repository.FindById(entityId);
            await repository.DeleteAsync(entity);
        }
        public async Task UpdateAsync(TEntityDto entity)
        {
            await repository.UpdateAsync(Mapper.Map<TEntity>(entity));
        }
        public async Task BulkUpsertAsync(IEnumerable<TEntityDto> entity)
        {
            await repository.BulkUpsertAsync(Mapper.Map<IEnumerable<TEntity>>(entity));
        }
        public IEnumerable<TEntityDto> GetAll()
        {
            loggerHelper.LogInfo(GetType().FullName, "Entidad consultada");
            return from book in repository.GetAll()
                   select Mapper.Map<TEntityDto>(book);
        }
        public TEntityDto FindById(TId id)
        {
            TEntity book = repository.FindById(id);
            return book != null ? Mapper.Map<TEntityDto>(book) : null;
        }
        public void Add(TEntityDto entity)
        {
            repository.Add(Mapper.Map<TEntity>(entity));
        }
        public void Update(TEntityDto entity)
        {
            repository.Update(Mapper.Map<TEntity>(entity));
        }
        public void Delete(TEntityDto entity)
        {
            repository.DeleteAsync(Mapper.Map<TEntity>(entity));
        }
        public void Delete(TId entityId)
        {
            TEntity entity = repository.FindById(entityId);
            repository.Delete(entity);
        }
    }
}
