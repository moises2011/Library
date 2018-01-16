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
        public Task<Book> FindByIdAsync(long id)
        {
            return unitOfWork.GetSet<Book>().FindAsync(id);
        }
        public async Task AddAsync(Book entity)
        {
            if (entity != null)
            {
                var books = unitOfWork.GetSet<Book>();
                await books.AddAsync(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task UpdateAsync(Book entity)
        {
            if (entity != null)
            {
                var book = unitOfWork.GetSet<Book>();
                book.Update(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task DeleteAsync(Book entity)
        {
            if (entity != null)
            {
                unitOfWork.GetSet<Book>().Remove(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task DeleteAsync(long id)
        {
            var address = FindById(id);
            if (address != null)
            {
                await DeleteAsync(address);
            }
        }
        public async Task BulkInsertAsync(IEnumerable<Book> books)
        {
            if (books != null && books.Any())
            {
                await unitOfWork.GetSet<Book>().AddRangeAsync(books);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task BulkUpsertAsync(IEnumerable<Book> books)
        {
            try
            {
                if (books != null && books.Any())
                {
                    //unitOfWork.SetAutoDetectChanges(false);

                    foreach (var entity in books)
                    {
                        unitOfWork.GetSet<Book>().UpdateRange(books);
                    }

                    await unitOfWork.CommitAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                //unitOfWork.SetAutoDetectChanges(true);
            }
        }
        
        public IEnumerable<Book> GetAll()
        {
            return unitOfWork.GetSet<Book>();
        }
        public Book FindById(long id)
        {
            return this.unitOfWork.GetSet<Book>().Find(id);
        }
        public void Add(Book entity)
        {
            if (entity != null)
            {
                var books = unitOfWork.GetSet<Book>();
                books.Add(entity);
                unitOfWork.Commit();
            }
        }
        public void Update(Book entity)
        {
            if (entity != null)
            {
                var book = unitOfWork.GetSet<Book>();
                book.Update(entity);
                unitOfWork.Commit();
            }
        }
        public void Delete(Book entity)
        {
            if (entity != null)
            {
                unitOfWork.GetSet<Book>().Remove(entity);
                unitOfWork.Commit();
            }
        }
        public void Delete(long id)
        {
            var address = FindById(id);
            if (address != null)
            {
                Delete(address);
            }
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
