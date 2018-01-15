using Library.Core;
using Library.Data.IRepositories;
using Library.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.Tests
{
    [TestClass]
    public class BookRepositoryTest
    {
        private IQueryableUnitOfWork unitOfWork;
        private IBookRepository bookRepository;
        [TestInitialize]
        public void Initialize()
        {
            MappingConfig.Initialize();
        }

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            IEnumerable<Book> books;
            List<Book> booksWait = new List<Book>();
            Book book = new Book { Name = "Book 1" };
            booksWait.Add(book);
            UnitOfWorkTest<Book> testExtensions = new UnitOfWorkTest<Book>();
            unitOfWork = testExtensions.SetQueryableUnitOfWork(book);
            bookRepository = new BookRepository(unitOfWork);
            //Act
            books = bookRepository.GetAll();
            //Assert
            Assert.AreEqual(books.ToList().Count, booksWait.Count);
        }
    }
}
