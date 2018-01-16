using Library.Core.Dtos;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IBookServices : IServices<long, Book>
    {
        Task AddAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task DeleteAsync(Book entity);
        Task DeleteAsync(int entityId);
        void Add(Book entity);
        void Update(Book entity);
        void Delete(Book entity);
        void Delete(int entityId);
    }
}
