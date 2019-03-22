using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {

        }

        public DbSet<TodoItem> ToDoItems { get; set; }
    }
}
