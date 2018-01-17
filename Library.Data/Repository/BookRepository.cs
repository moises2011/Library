using Library.Data.IRepositories;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public class BookRepository : ERepository<long, Book>, IBookRepository
    {
        private readonly IQueryableUnitOfWork unitOfWork;
        public BookRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
