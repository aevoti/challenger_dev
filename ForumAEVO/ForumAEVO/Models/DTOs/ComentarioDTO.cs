using System.Text.Json.Serialization;

namespace ForumAEVO.Models.DTOs
{
    public class ComentarioDto
    {
       
        public Guid Id { get; set; }
        public Guid TopicoId { get; set; }
        public Guid UserId { get; set; }
        public string Msg { get; set; } =string.Empty;
        [JsonIgnore]
        public string Data { get; set; } = string.Empty;
    }

}
