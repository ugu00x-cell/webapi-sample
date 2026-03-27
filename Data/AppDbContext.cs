using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

// Flask の db = SQLAlchemy(app) に相当
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Flask の class Todo(db.Model) に相当
    // テーブル名は自動で "Todos" になる
    public DbSet<Todo> Todos => Set<Todo>();
}
