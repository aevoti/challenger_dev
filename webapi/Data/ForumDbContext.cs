using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Data
{

    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Usuario> Users { get; set; }

    }

}
