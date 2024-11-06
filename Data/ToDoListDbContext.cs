using Microsoft.EntityFrameworkCore;
using ToDoListPaczkowski.Model;

namespace ToDoListPaczkowski.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
    : base(options)
        {
        }

        public DbSet<TaskToDo> taskToDo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
