using System.Threading.Tasks;
using Library.Data;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Library.Data
{
    public class LibraryContext : DbContext, IQueryableUnitOfWork
    {

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Entity> GetSet<Entity>() where Entity : EntityBase
        {
            return Set<Entity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
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
