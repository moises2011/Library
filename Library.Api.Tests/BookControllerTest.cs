using Library.Core;
using Library.Data.IRepositories;
using Library.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Library.Data;
using Library.Core.Interfaces;
using Library.Core.Services;
using Library.Data.Repository;
using Library.Api.Controllers;

namespace Library.Api.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        private IQueryableUnitOfWork unitOfWork;
        private IBookRepository bookRepository;
        private IBookService bookService;
        private BooksController bookApi;

        private Book entity;
        private Core.Dtos.Book entityDto;
        private List<Book> entitiesWait;
        private List<Core.Dtos.Book> entitiesDto;

        [TestInitialize]
        public void Initialize()
        {
            Mapper.Reset();
            MappingConfig.Initialize();

            entity = new Book { Name = "Book 1", Amount = 1, Price = 1 };
            entityDto = Mapper.Map<Core.Dtos.Book>(entity);
            entitiesWait = new List<Book>();
        }

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            entitiesDto = null;
            entitiesWait.Add(entity);
            entitiesWait.Add(entity);
            bookApi = SetController<Book, long>(entitiesWait);
            //Act
            entitiesDto = bookApi.GetAll().ToList();
            //Assert
            Assert.AreEqual(entitiesDto.Count, entitiesWait.Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            //Arrange
            entityDto = null;
            entity.Id = 1;
            entitiesWait.Add(entity);
            bookApi = SetController<Book, long>(entity);
            //Act
            entityDto = bookApi.FindById(entity.Id);
            //Assert
            Assert.AreEqual(entityDto.Id, entity.Id);
        }

        public BooksController SetController<T, TId>(List<T> entities) where TId : struct where T : EntityBase<TId>
        {
            UnitOfWorkTest<T, TId> testExtensions = new UnitOfWorkTest<T, TId>();
            unitOfWork = testExtensions.SetQueryableUnitOfWork(entities);
            bookRepository = new BookRepository(unitOfWork);
            bookService = new BookService(bookRepository);
            return new BooksController(bookService);
        }

        public BooksController SetController<T, TId>(T entity) where TId : struct where T : EntityBase<TId>
        {
            UnitOfWorkTest<T, TId> testExtensions = new UnitOfWorkTest<T, TId>();
            unitOfWork = testExtensions.SetQueryableUnitOfWork(entity);
            bookRepository = new BookRepository(unitOfWork);
            bookService = new BookService(bookRepository);
            return new BooksController(bookService);
        }
    }
}
