using System.Text.Json.Serialization;

namespace ForumAEVO.Models.DTOs
{
    public class ComentarioDto
    {
       
        public int Id { get; set; }
        public int TopicoId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string DonoDaPostagem { get; set; } = string.Empty;
        public string Foto { get; set; }= string.Empty;
        public string Msg { get; set; } =string.Empty;
        public string Data { get; set; } = string.Empty;
    }

}
