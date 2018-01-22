using Library.Core.Dtos;
using Library.Core.Interfaces;
using Library.Data.Helpers;
using Library.Data.IRepositories;

namespace Library.Core.Services
{
    public class BookService : Service<long, Data.Entities.Book, Book>, IBookService
    {
        private readonly IBookRepository repository;

        public BookService(IBookRepository repository) : base(repository, new LoggerHelper())
        {
            this.repository = repository;
        }
    }
}
