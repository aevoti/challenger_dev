namespace ForumMinimalAPI.Models
{
    public class Topico
    {
        public int Id { get; set; }
        public DateTime DataHoraPostagem { get; set; }
        public int UsuarioId { get; set; }
        public required string Conteudo { get; set; }
        public List<Curtida>? Curtidas {  get; set; }
        public List<Comentario>? Comentarios { get; set; }
    }
}