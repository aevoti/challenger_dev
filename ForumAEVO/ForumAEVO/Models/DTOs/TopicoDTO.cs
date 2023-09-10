namespace ForumAEVO.Models.DTOs
{
    public class TopicoDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Msg { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public List<ComentarioDto>? Comentarios { get; set; }
    }
}
