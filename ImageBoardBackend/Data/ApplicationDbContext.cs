using Microsoft.EntityFrameworkCore;
using ImageBoard.Models;

namespace ImageBoard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("posts");  // Force lowercase table name
            modelBuilder.Entity<Post>().HasIndex(p => p.Category);
        }
    }
}