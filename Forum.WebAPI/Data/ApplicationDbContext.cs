using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() {}
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<Topico> Topicos {get; set;}
    public DbSet<Comentario> Comentarios {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=forum.db"); // Nome do arquivo do banco de dados SQLite
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasData(new List<Usuario>{
                new Usuario(1, "Gustavo Andrade", "gustavo@gmail.com", "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNk+M9Qz0AEYBxVSF+FAAhKDveksOjmAAAAAElFTkSuQmCC"),
                new Usuario(2, "Nelson Silva", "nelso@gmail.com", "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mP8z8BQz0AEYBxVSF+FABJADveWkH6oAAAAAElFTkSuQmCC"),
                new Usuario(3, "Maria Almeida", "mari@gmail.com", "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNkYPhfz0AEYBxVSF+FAP5FDvcfRYWgAAAAAElFTkSuQmCC")
            });
            
        modelBuilder.Entity<Topico>()
            .HasData(new List<Topico>{
                new Topico(1, "Carros", "Carros sao caros", 1, DateTime.Now),
                new Topico(2, "Comida", "Comidas sao cozidas", 2, DateTime.Now),
                new Topico(3, "Materiais Escolares", "Canetas bic", 2, DateTime.Now)
            });

    }
}
