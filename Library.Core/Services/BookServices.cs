using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Core.Dtos;
using Library.Core.Interfaces;
using Library.Data.IRepositories;
using AutoMapper;
using System.Linq;

namespace Library.Core.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository repository;

        public BookServices(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task Add(Book entity)
        {
            await repository.Add(Mapper.Map<Data.Entities.Book>(entity));
        }

        public async Task Delete(Book entity)
        {
            await repository.Delete(Mapper.Map<Data.Entities.Book>(entity));
        }

        public async Task Delete(int entityId)
        {
            Data.Entities.Book entity = repository.FindById(entityId);
            await repository.Delete(Mapper.Map<Data.Entities.Book>(entity));
        }

        public async Task Update(Book entity)
        {
            repository.Update(Mapper.Map<Data.Entities.Book>(entity));
        }

        public Book FindById(long id)
        {
            Data.Entities.Book book = repository.FindById(id);
            return book != null ? Mapper.Map<Book>(book) : null;
        }

        public async Task<Book> FindByIdAsync(long id)
        {
            Data.Entities.Book book = await repository.FindByIdAsync(id);
            return book != null ? Mapper.Map<Book>(book) : null;
        }

        public IEnumerable<Book> GetAll()
        {
            return from book in repository.GetAll()
                   select Mapper.Map<Book>(book);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return from book in await repository.GetAllAsync()
                   select Mapper.Map<Book>(book);
        }

        Task IBookServices.Update(Book entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
