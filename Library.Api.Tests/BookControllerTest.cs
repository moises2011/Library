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
using Library.Controllers;

namespace Library.Api.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        private IQueryableUnitOfWork unitOfWork;
        private IBookRepository bookRepository;
        private IBookServices bookService;
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
            bookApi = SetController<Book>(entitiesWait);
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
            bookApi = SetController<Book>(entity);
            //Act
            entityDto = bookApi.FindById(entity.Id);
            //Assert
            Assert.AreEqual(entityDto.Id, entity.Id);
        }

        public BooksController SetController<T>(List<T> entities) where T : EntityBase
        {
            UnitOfWorkTest<T> testExtensions = new UnitOfWorkTest<T>();
            unitOfWork = testExtensions.SetQueryableUnitOfWork(entities);
            bookRepository = new BookRepository(unitOfWork);
            bookService = new BookServices(bookRepository);
            return new BooksController(bookService);
        }

        public BooksController SetController<T>(T entity) where T : EntityBase
        {
            UnitOfWorkTest<T> testExtensions = new UnitOfWorkTest<T>();
            unitOfWork = testExtensions.SetQueryableUnitOfWork(entity);
            bookRepository = new BookRepository(unitOfWork);
            bookService = new BookServices(bookRepository);
            return new BooksController(bookService);
        }
    }
}
