using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IQueryableUnitOfWork unitOfWork;

        public BookRepository(IQueryableUnitOfWork libraryContext)
        {
            unitOfWork = libraryContext;
        }

        public IQueryableUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await unitOfWork.GetSet<Book>()
                .OrderBy(e => e.Name).ToListAsync();
        }

        public IEnumerable<Book> GetAll()
        {
            return unitOfWork.GetSet<Book>();
        }

        public Task<Book> FindByIdAsync(long id)
        {
            return unitOfWork.GetSet<Book>().FindAsync(id);
        }

        public Book FindById(long id)
        {
            return this.unitOfWork.GetSet<Book>().Find(id);
        }

        public async Task Add(Book entity)
        {
            if (entity != null)
            {
                var books = unitOfWork.GetSet<Book>();
                await books.AddAsync(entity);
            }
        }

        public async Task Update(Book entity)
        {
            if (entity != null)
            {
                var address = unitOfWork.GetSet<Book>();
                address.Update(entity);
            }
        }

        public async Task Delete(Book entity)
        {
            if (entity != null)
            {
                unitOfWork.GetSet<Book>().Remove(entity);
                unitOfWork.Commit();
            }
        }
        public async Task Delete(long id)
        {
            var address = FindById(id);
            if (address != null)
            {
                await Delete(address);
            }
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
