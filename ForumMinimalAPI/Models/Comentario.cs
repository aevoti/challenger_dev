namespace ForumMinimalAPI.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public int TopicoId { get; set; }
        public int UsuarioId { get; set; }
        public required string Conteudo { get; set; }
    }
}