using Library.Data;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public interface IRepository<TId, TEntity> : IDisposable
        where TId : struct
        where TEntity : EntityBase
    {
        IQueryableUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        Task<TEntity> FindByIdAsync(TId id);
        TEntity FindById(TId id);
    }
}
