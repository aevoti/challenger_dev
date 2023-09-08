namespace ForumAEVO.Models.DTOs
{
    public class ForumDTO
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Msg { get; set; } = string.Empty;

        public string Data { get; set; } = string.Empty;

        public List<ComentarioDto>? Comentarios { get; set; }
    }
}

