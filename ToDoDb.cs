


using Microsoft.EntityFrameworkCore;

class ToDoDb: DbContext
{
    public ToDoDb(DbContextOptions<ToDoDb> options)
    : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
  
}