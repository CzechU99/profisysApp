using Microsoft.EntityFrameworkCore;
using profisysApp.Entities;

namespace profisysApp.Data
{

    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Documents> Documents { get; set; }
        public DbSet<DocumentItems> DocumentItems { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>()
                .HasMany(d => d.DocumentItem)
                .WithOne(i => i.Document)
                .HasForeignKey(i => i.DocumentId);
        }
    
    }

}
