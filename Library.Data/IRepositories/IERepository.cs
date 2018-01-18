using Library.Data;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public interface IERepository<TId, TEntity> : IDisposable 
        where TId : struct 
        where TEntity : EntityBase
    {
        IQueryableUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> FindByIdAsync(TId id);
        TEntity FindById(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TId id);
        Task BulkUpsertAsync(IEnumerable<TEntity> entity);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
    }
}
