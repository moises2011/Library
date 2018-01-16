using System.Threading.Tasks;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Library.Data
{
    public class LibraryContext : DbContext, IQueryableUnitOfWork
    {
        private readonly string Schema;
        public LibraryContext(DbContextOptions<LibraryContext> options, string schema) : base(options)
        {
            Schema = schema;
        }

        public DbSet<Entity> GetSet<Entity>() where Entity : EntityBase
        {
            return Set<Entity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.Entity<Book>().ToTable("Book");

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
    }
}
