using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ForumAEVO.Models
{
    [Table("Comentario")]
    public class Comentario : Forum
    {
        //A chave estrangeira que se relaciona com topico ID
        public int TopicoId { get; set; }

        // A chave estrangeira que relaciona o tópico ao usuário
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        [JsonIgnore] 
        public Usuario? Usuario { get; set; }

        [ForeignKey("TopicoId")]
        [JsonIgnore] 
        public Topico? Topicos { get; set; }


    }

}
