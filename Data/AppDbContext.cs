using Microsoft.EntityFrameworkCore;
using MyToDoList.Models;

namespace MyToDoList.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        // Constructor required for dependency injection
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
