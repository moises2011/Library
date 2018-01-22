using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IQueryableUnitOfWork : IDisposable
    {
        DbSet<TEntity> GetSet<TEntity, TId>() where TId : struct where TEntity : EntityBase<TId>;
        void Commit();
        Task CommitAsync();
        void SetAutoDetectChanges(bool autoDetect);
    }
}
