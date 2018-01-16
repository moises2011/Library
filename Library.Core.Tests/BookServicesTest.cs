using AutoMapper;
using Library.Core.Interfaces;
using Library.Core.Services;
using Library.Data.IRepositories;
using Library.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Tests
{
    [TestClass]
    public class BookServicesTest
    {
        private IBookRepository bookRepository;
        private IBookServices bookServices;

        private Book entity;
        private Dtos.Book entityDto;
        private List<Book> entitiesWait;
        private List<Dtos.Book> entitiesDto;

        [TestInitialize]
        public void Initialize()
        {
            Mapper.Reset();
            MappingConfig.Initialize();
            bookRepository = Substitute.For<IBookRepository>();
            bookServices = new BookServices(bookRepository);
            entity = new Book { Name = "Book 1", Amount = 1, Price = 1 };
            entityDto = Mapper.Map<Dtos.Book>(entity);
            entitiesWait = new List<Book>();
        } 

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            entitiesDto = null;
            entitiesWait.Add(entity);
            bookRepository.GetAll().Returns(entitiesWait);
            //Act
            entitiesDto = bookServices.GetAll().ToList();
            //Assert
            Assert.AreEqual(entitiesDto.ToList().Count,entitiesWait.Count);
        }

        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            //Arrange
            entitiesDto = null;
            entitiesWait.Add(entity);
            bookRepository.GetAllAsync().Returns(entitiesWait);
            //Act
            entitiesDto = (await bookServices.GetAllAsync()).ToList();
            //Assert
            Assert.AreEqual(entitiesDto.Count, entitiesWait.Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            //Arrange
            Book entityWait = entity;
            entityDto = null;
            bookRepository.FindById(Arg.Any<long>()).Returns(entity);
            //Act
            entityDto = bookServices.FindById(1);
            //Assert
            Assert.AreEqual(entityDto.Id, entityWait.Id);
        }

        [TestMethod]
        public async Task FindByIdAsyncTest()
        {
            //Arrange
            Book entityWait = entity;
            entityDto = null;
            bookRepository.FindByIdAsync(Arg.Any<long>()).Returns(entity);
            //Act
            entityDto = await bookServices.FindByIdAsync(1);
            //Assert
            Assert.AreEqual(entityDto.Id, entityWait.Id);
        }

    }
}
