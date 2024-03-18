using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasIndex(u => u.Title).IsUnique();
        }
    }
}
