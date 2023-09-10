using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ForumAEVO.Models
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Topico> Topicos { get; set; }

    }
}
