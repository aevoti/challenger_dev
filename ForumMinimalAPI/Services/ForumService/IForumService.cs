using ForumMinimalAPI.Models;

namespace ForumMinimalAPI.Services.ForumService
{
    public interface IForumService
    {
        Task<List<Topico>> ObterTopicos(string filtroTexto, string ordenacao);
        Task<Topico> ObterTopicoPorId(int id);
        Task<Topico> CadastrarTopico(Topico topico);
        Task<Topico> AtualizarTopico(int id, Topico topico);
        Task<bool> DeletarTopico(int id);
        Task<bool> CurtirOuDescurtirTopico(Curtida curtida);
        Task<Comentario> CadastrarComentario(int topicoId, Comentario comentario);
        Task<Comentario> AtualizarComentario(int topicoId, int usuarioId, Comentario comentario);
        Task<bool> DeletarComentario(int topicoId, int usuarioId, Comentario comentario);
    }
}