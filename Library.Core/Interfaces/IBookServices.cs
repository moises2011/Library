using Library.Core.Dtos;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IBookServices : IServices<long, Book>
    {
        Task Add(Book entity);
        Task Update(Book entity);
        Task Delete(Book entity);
        Task Delete(int entityId);
    }
}
