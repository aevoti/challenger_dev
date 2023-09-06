using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;

namespace Forum.WebAPI.Models;

public class Comentario
{
    public Comentario(int id, string descricao, int usuarioId, int topicoId) 
    {
        this.Id = id;
        this.Descricao = descricao;
        this.UsuarioId = usuarioId;
        this.TopicoId = topicoId;
    }
    public int Id { get; set; }
    public string Descricao { get; set; }
    public int UsuarioId { get; set; } // Chave estrangeira para o autor
    public Usuario Usuario { get; set; } // Autor do comentário
    public int TopicoId { get; set; } // Chave estrangeira para o tópico ao qual o comentário pertence
    public Topico Topico { get; set; } // Tópico ao qual o comentário pertence

}
