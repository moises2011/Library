using System.Threading.Tasks;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

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
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        public Task CommitAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
