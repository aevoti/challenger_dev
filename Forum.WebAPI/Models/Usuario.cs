using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebAPI.Models;

public class Usuario
{
    public Usuario(int id, string name, string email, string photo) 
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.Photo = photo;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Photo { get; set; }
    public ICollection<Topico> Topicos { get; set; } // Tópicos criados pelo usuário
    public ICollection<Comentario> Comentarios { get; set; } // Comentários feitos pelo usuário
}
