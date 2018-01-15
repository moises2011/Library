using Library.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public interface IBookRepository : IRepository<long, Book>
    {
        Task Add(Book entity);
        Task Update(Book entity);
        Task Delete(Book entity);
        Task Delete(long id);
    }
}
