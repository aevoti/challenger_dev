using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebAPI.Models
{
    public class Comentario
    {
        public Comentario(int id, DateTime criadoEm, Usuario usuario, Topico topico) 
        {
            this.Id = id;
            this.CriadoEm = criadoEm;
            this.Usuario = usuario;
            this.Topico = topico;

        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public int UsuarioId { get; set; } // Chave estrangeira para o autor
        public Usuario Usuario { get; set; } // Autor do comentário
        public int TopicoId { get; set; } // Chave estrangeira para o tópico ao qual o comentário pertence
        public Topico Topico { get; set; } // Tópico ao qual o comentário pertence

    }
}