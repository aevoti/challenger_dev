using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebAPI.Models;

public class Topico
{
    public Topico () { }
    public Topico(int id, string titulo, string descricao, int usuarioId, DateTime dataCriacao) 
    {
        this.Id = id;
        this.Titulo = titulo;
        this.Descricao = descricao;
        this.DataCriacao = dataCriacao;
        this.UsuarioId = usuarioId;
    }
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int UsuarioId { get; set; } // Chave estrangeira para o autor
    public Usuario? Usuario { get; set; } // Autor do tópico
    public ICollection<Comentario>? Comentarios { get; set; } // Comentários neste tópico
}
