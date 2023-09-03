namespace ForumAEVO.Models.DTOs
{
    public class ComentarioDto
    {
        public Guid TopicoId { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Msg { get; set; } =string.Empty;
    }

}
