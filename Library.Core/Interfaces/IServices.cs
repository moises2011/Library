using Library.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IServices<TId, TEntity> where TId : struct where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        Task<TEntity> FindByIdAsync(TId id);
        TEntity FindById(TId id);
    }
}
