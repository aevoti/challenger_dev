using Forum.ConcreteProduct;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        {
            
        }
        public DbSet<Topico> Topico { get; set; } = null!;
        public DbSet<Comentario> Comentario { get; set; } = null!;

    }
}
