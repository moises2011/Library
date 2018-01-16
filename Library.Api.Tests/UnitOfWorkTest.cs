using Library.Data;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Library.Api.Tests
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWorkTest<T> where T : EntityBase
    {
        public IQueryableUnitOfWork SetQueryableUnitOfWork(List<T> model)
        {
            IQueryableUnitOfWork queryableUnitOfWork = Substitute.For<IQueryableUnitOfWork>();
            var data = model;

            DbSet<T> dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            dbSet.AddRange(model);

            var asQuerable = data.AsQueryable();

            ((IQueryable<T>)dbSet).Provider.Returns(asQuerable.Provider);
            ((IQueryable<T>)dbSet).Expression.Returns(asQuerable.Expression);
            ((IQueryable<T>)dbSet).ElementType.Returns(asQuerable.ElementType);
            ((IQueryable<T>)dbSet).GetEnumerator().Returns(asQuerable.GetEnumerator());

            queryableUnitOfWork.GetSet<T>().Returns(dbSet);
            return queryableUnitOfWork;
        }

        public IQueryableUnitOfWork SetQueryableUnitOfWork(T model)
        {
            IQueryableUnitOfWork queryableUnitOfWork = Substitute.For<IQueryableUnitOfWork>();
            var data = new List<T>();
            data.Add(model);

            DbSet<T> dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            dbSet.Add(model);

            var asQuerable = data.AsQueryable();

            ((IQueryable<T>)dbSet).Provider.Returns(asQuerable.Provider);
            ((IQueryable<T>)dbSet).Expression.Returns(asQuerable.Expression);
            ((IQueryable<T>)dbSet).ElementType.Returns(asQuerable.ElementType);
            ((IQueryable<T>)dbSet).GetEnumerator().Returns(asQuerable.GetEnumerator());

            queryableUnitOfWork.GetSet<T>().Returns(dbSet);
            dbSet.Find(Arg.Any<long>()).Returns(model);

            return queryableUnitOfWork;
        }
    }
}
