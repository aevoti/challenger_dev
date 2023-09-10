using System.Text.Json.Serialization;

namespace ForumAEVO.Models.DTOs
{
    public class TopicoDto
    {
        [JsonIgnore]
        public int Id { get; set; }
       
        public Guid UserId { get; set; }
        public string Msg { get; set; } = string.Empty;
        [JsonIgnore] 
        public string Data { get; set; } = string.Empty;

        [JsonIgnore]
        public List<ComentarioDto>? Comentarios { get; set; }
    }
}
