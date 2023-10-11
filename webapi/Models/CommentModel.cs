using System.Text.Json.Serialization;

namespace webapi.Models
{
    public class CommentModel
    {        
        public string Content { get; set; }
        public int? UsersId { get; set; } // Adicione esta propriedade para representar o autor do comentário
    }
}
