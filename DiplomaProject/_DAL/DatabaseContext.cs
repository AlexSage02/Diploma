using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_DAL;
using System.Reflection.Emit;

namespace DAL
{
    public class DatabaseContext : IdentityDbContext 
    {      
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<MapEvent> MapEvents { get; set; } = null!;

		public DatabaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Events.db");
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Event>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<MapEvent>()
                .HasIndex(c => c.Name)
                .IsUnique();
            base.OnModelCreating(builder);
        }
    }
}
