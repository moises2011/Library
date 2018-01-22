using Library.Data.Entities;
using Library.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public class ERepository<TId, TEntity> : IERepository<TId, TEntity> 
        where TId : struct 
        where TEntity : EntityBase 
    {
        private readonly IQueryableUnitOfWork unitOfWork;

        public ERepository(IQueryableUnitOfWork libraryContext)
        {
            unitOfWork = libraryContext;
        }

        public IQueryableUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = unitOfWork.GetSet<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await unitOfWork.GetSet<TEntity>().ToListAsync();
        }
        public Task<TEntity> FindByIdAsync(TId id)
        {
            return unitOfWork.GetSet<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            if (entity != null)
            {
                var item = unitOfWork.GetSet<TEntity>();
                await item.AddAsync(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity != null)
            {
                var book = unitOfWork.GetSet<TEntity>();
                book.Update(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                unitOfWork.GetSet<TEntity>().Remove(entity);
                await unitOfWork.CommitAsync();
            }
        }
        public async Task DeleteAsync(TId id)
        {
            var address = FindById(id);
            if (address != null)
            {
                await DeleteAsync(address);
            }
        }
        public async Task BulkUpsertAsync(IEnumerable<TEntity> entity)
        {
            try
            {
                if (entity != null && entity.Any())
                {
                    unitOfWork.SetAutoDetectChanges(true);

                    foreach (TEntity item in entity)
                    {
                        await unitOfWork.GetSet<TEntity>().AddAsync(item);
                        //if (item.Id > 0 && unitOfWork.GetSet<TEntity>().Any(info =>
                        //        info.Id == item.Id))
                        //{
                        //    unitOfWork.GetSet<TEntity>().Update(item);
                        //}
                        //else
                        //{
                        //    await unitOfWork.GetSet<TEntity>().AddAsync(item);
                        //}
                    }

                    await unitOfWork.CommitAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                //unitOfWork.SetAutoDetectChanges(true);
            }
        }
        
        public IEnumerable<TEntity> GetAll()
        {
            return unitOfWork.GetSet<TEntity>();
        }
        public TEntity FindById(TId id)
        {
            return this.unitOfWork.GetSet<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            if (entity != null)
            {
                var item = unitOfWork.GetSet<TEntity>();
                item.Add(entity);
                unitOfWork.Commit();
            }
        }
        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                var item = unitOfWork.GetSet<TEntity>();
                item.Update(entity);
                unitOfWork.Commit();
            }
        }
        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                unitOfWork.GetSet<TEntity>().Remove(entity);
                unitOfWork.Commit();
            }
        }
        public void Delete(TId id)
        {
            var item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

        
    }
}
