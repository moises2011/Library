using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Core.Dtos;
using Library.Core.Interfaces;
using Library.Data.IRepositories;
using AutoMapper;
using System.Linq;
using Library.Data.Helpers;

namespace Library.Core.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository repository;
        private readonly ILoggerHelper loggerHelper;

        public BookServices(IBookRepository repository, ILoggerHelper _loggerHelper)
        {
            this.repository = repository;
            loggerHelper = _loggerHelper;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return from book in await repository.GetAllAsync()
                   select Mapper.Map<Book>(book);
        }
        public async Task<Book> FindByIdAsync(long id)
        {
            Data.Entities.Book book = await repository.FindByIdAsync(id);
            return book != null ? Mapper.Map<Book>(book) : null;
        }
        public async Task AddAsync(Book entity)
        {
            await repository.AddAsync(Mapper.Map<Data.Entities.Book>(entity));
        }
        public async Task DeleteAsync(Book entity)
        {
            await repository.DeleteAsync(Mapper.Map<Data.Entities.Book>(entity));
        }
        public async Task DeleteAsync(long entityId)
        {
            Data.Entities.Book entity = repository.FindById(entityId);
            await repository.DeleteAsync(Mapper.Map<Data.Entities.Book>(entity));
        }
        public async Task UpdateAsync(Book entity)
        {
            await repository.UpdateAsync(Mapper.Map<Data.Entities.Book>(entity));
        }

        public IEnumerable<Book> GetAll()
        {
            loggerHelper.LogInfo(GetType().FullName, "Libros consultados");
            return from book in repository.GetAll()
                   select Mapper.Map<Book>(book);
        }
        public Book FindById(long id)
        {
            Data.Entities.Book book = repository.FindById(id);
            return book != null ? Mapper.Map<Book>(book) : null;
        }
        public void Add(Book entity)
        {
            repository.Add(Mapper.Map<Data.Entities.Book>(entity));
        }
        public void Update(Book entity)
        {
            repository.Update(Mapper.Map<Data.Entities.Book>(entity));
        }
        public void Delete(Book entity)
        {
            repository.DeleteAsync(Mapper.Map<Data.Entities.Book>(entity));
        }
        public void Delete(long entityId)
        {
            Data.Entities.Book entity = repository.FindById(entityId);
            repository.Delete(Mapper.Map<Data.Entities.Book>(entity));
        }
    }
}
