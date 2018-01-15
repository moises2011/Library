using AutoMapper;
using Library.Core.Interfaces;
using Library.Core.Services;
using Library.Data.IRepositories;
using Library.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;


namespace Library.Core.Tests
{
    [TestClass]
    public class BookServicesTest
    {
        private IBookRepository bookRepository;
        private IBookServices bookServices;
       

        [TestInitialize]
        public void Initialize()
        {
            bookRepository = Substitute.For<IBookRepository>();
            bookServices = new BookServices(bookRepository);
            MappingConfig.Initialize();
        } 

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            IEnumerable<Dtos.Book> books;
            List<Book> booksWait = new List<Book>();
            booksWait.Add(new Book { Name="Book 1"});
            bookRepository.GetAll().Returns(booksWait);
            //Act
            books = bookServices.GetAll();
            //Assert
            Assert.AreEqual(books.ToList().Count,booksWait.Count);
        }
    }
}
