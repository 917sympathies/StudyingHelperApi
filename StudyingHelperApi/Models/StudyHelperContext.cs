using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudyingHelperApi.Models
{
    public class StudyHelperContext : DbContext
    {
        public StudyHelperContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Username);
        }

        public DbSet<User> users { get; set; }
        public DbSet<Workspace> workspaces { get; set; }
        public DbSet<Task> tasks { get; set; }
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<StudyHelperContext>
    {
        public StudyHelperContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudyHelperContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User ID=postgres;Password=123;Database=StudyHelperDatabase;");

            return new StudyHelperContext(optionsBuilder.Options);
        }
    }
}
