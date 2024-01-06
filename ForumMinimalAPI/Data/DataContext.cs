using ForumMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumMinimalAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=PREDATORMOON;Database=forumaevodb;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Topico> Topicos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
    }
}