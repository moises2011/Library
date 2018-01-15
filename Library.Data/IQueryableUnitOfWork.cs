using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IQueryableUnitOfWork : IDisposable
    {
        DbSet<Entity> GetSet<Entity>() where Entity : EntityBase;
        void Commit();
        Task CommitAsync();
    }
}
