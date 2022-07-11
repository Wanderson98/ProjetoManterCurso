using ManterCursosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ManterCursosApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
