using Microsoft.EntityFrameworkCore;

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
}
