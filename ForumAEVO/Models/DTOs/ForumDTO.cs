using System.Text.Json.Serialization;

namespace ForumAEVO.Models.DTOs
{
    public class ForumDTO
    {

        public int Id { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string DonoDaPostagem { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Msg { get; set; } = string.Empty;

        public string Data { get; set; } = string.Empty;

        public List<ComentarioDto>? Comentarios { get; set; }
    }
}

