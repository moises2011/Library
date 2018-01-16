using Library.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public interface IBookRepository : IRepository<long, Book>
    {
        Task AddAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task DeleteAsync(Book entity);
        Task DeleteAsync(long id);
        void Add(Book entity);
        void Update(Book entity);
        void Delete(Book entity);
        void Delete(long id);
    }
}
