using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ForumAEVO.Models
{
    [Table("Topico")]
    public class Topico : Forum
    {

        // A chave estrangeira que relaciona o tópico ao usuário
        public Guid UserId { get; set; }

        // armazena o usuário quem criou o topico
        [ForeignKey("UserId")]
        [JsonIgnore] 
        public Usuario? Usuario { get; set; }

        // Um Tópico pode ter zero ou muitos comentários
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
