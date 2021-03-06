﻿using System.Threading.Tasks;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Library.Data
{
    public class Context : DbContext, IQueryableUnitOfWork
    {
        private readonly string Schema;
        public Context(DbContextOptions<Context> options, string schema) : base(options)
        {
            Schema = schema;
        }

        public DbSet<TEntity> GetSet<TEntity, TId>() where TId : struct where TEntity : EntityBase<TId>
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Category>().ToTable("Category");

            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
            //catch (DbUpdateException ex)
            //{
            //    //logger.Log(LogLevel.Error, ex.Message, ex);
            //    throw;
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        logger.Log(LogLevel.Critical, "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            logger.Log(LogLevel.Critical, "- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }

            //    throw;
            //}
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
            //catch (DbUpdateException ex)
            //{
            //    //logger.Log(LogLevel.Error, ex.Message, ex);
            //    throw;
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        logger.Log(LogLevel.Critical, "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            logger.Log(LogLevel.Critical, "- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }

            //    throw;
            //}
        }
        public void SetAutoDetectChanges(bool autoDetect)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = autoDetect;
        }
    }
}
