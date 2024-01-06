using ForumMinimalAPI.Data;
using ForumMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumMinimalAPI.Services.ForumService
{
    public class ForumService : IForumService
    {
        private readonly DataContext _dataContext;

        public ForumService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Topico>> ObterTopicos(string filtroTexto, string ordenacao)
        {
            var query = _dataContext.Topicos.AsQueryable();

            if (!string.IsNullOrEmpty(filtroTexto))
            {
                query = query.Where(t => t.Conteudo.Contains(filtroTexto));
            }

            if (!string.IsNullOrEmpty(ordenacao))
            {
                switch (ordenacao.ToUpper())
                {
                    case "ASC":
                        query = query.OrderBy(t => t.DataHoraPostagem);
                        break;
                    case "DESC":
                        query = query.OrderByDescending(t => t.DataHoraPostagem);
                        break;
                }
            }

            var topicos = await query.ToListAsync();

            if (topicos.Count == 0)
                throw new InvalidOperationException("Não há tópicos para serem listados.");

            foreach (var topico in topicos)
            {
                topico.Comentarios = await _dataContext.Comentarios
                    .Where(c => c.TopicoId == topico.Id)
                    .ToListAsync();

                topico.Curtidas = await _dataContext.Curtidas
                    .Where(c => c.TopicoId == topico.Id)
                    .ToListAsync();
            }

            return topicos;
        }

        public async Task<Topico> ObterTopicoPorId(int id)
        {
            var topico = await _dataContext.Topicos.FindAsync(id);
            if (topico is null)
                throw new InvalidOperationException("O tópico não foi encontrado.");

            topico.Comentarios = await _dataContext.Comentarios
                .Where(c => c.TopicoId == id)
                .ToListAsync();

            return topico;
        }

        public async Task<Topico> CadastrarTopico(Topico topico)
        {
            if (string.IsNullOrEmpty(topico.Conteudo))
                throw new ArgumentException("O campo 'conteudo' não pode estar vazio.");

            topico.DataHoraPostagem = DateTime.Now;

            _dataContext.Topicos.Add(topico);
            await _dataContext.SaveChangesAsync();
            return topico;
        }


        public async Task<Topico> AtualizarTopico(int id, Topico topicoRequisicao)
        {
            var topico = await _dataContext.Topicos.FindAsync(id);
            if (topico is null)
                throw new InvalidOperationException("O tópico não foi encontrado.");

            if (string.IsNullOrEmpty(topico.Conteudo))
                throw new ArgumentException("O campo 'conteudo' não pode estar vazio.");

            topico.Conteudo = topicoRequisicao.Conteudo;
            await _dataContext.SaveChangesAsync();

            return topico;
        }

        public async Task<bool> DeletarTopico(int id)
        {
            var topico = await _dataContext.Topicos.FindAsync(id);
            if (topico is null)
                throw new InvalidOperationException("O tópico não foi encontrado.");

            var comentariosRelacionados = await _dataContext.Comentarios
                .Where(c => c.TopicoId == id)
                .ToListAsync();

            _dataContext.Comentarios.RemoveRange(comentariosRelacionados);

            _dataContext.Topicos.Remove(topico);

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CurtirOuDescurtirTopico(Curtida curtida)
        {
            var existingCurtida = _dataContext.Curtidas
                .FirstOrDefault(c => c.TopicoId == curtida.TopicoId && c.UsuarioId == curtida.UsuarioId);

            if (existingCurtida == null)
            {
                _dataContext.Curtidas.Add(curtida);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            else
            {
                _dataContext.Curtidas.Remove(existingCurtida);
                await _dataContext.SaveChangesAsync();
                return false;
            }
        }

        public async Task<Comentario> CadastrarComentario(int topicoId, Comentario comentario)
        {
            var topico = await _dataContext.Topicos.FindAsync(topicoId);

            if (topico == null)
                throw new ArgumentException("O tópico com o Id especificado não existe.");

            ValidarConteudoComentario(comentario.Conteudo);

            comentario.TopicoId = topicoId;

            _dataContext.Comentarios.Add(comentario);
            await _dataContext.SaveChangesAsync();

            return comentario;
        }

        public async Task<Comentario> AtualizarComentario(int topicoId, int usuarioId, Comentario comentarioRequisicao)
        {
            var comentario = await ObterEValidarComentario(comentarioRequisicao.Id, topicoId, usuarioId, "atualizar");

            ValidarConteudoComentario(comentario.Conteudo);

            comentario.Conteudo = comentarioRequisicao.Conteudo;
            await _dataContext.SaveChangesAsync();

            return comentario;
        }

        public async Task<bool> DeletarComentario(int topicoId, int usuarioId, Comentario comentarioRequisicao)
        {
            var comentario = await ObterEValidarComentario(comentarioRequisicao.Id, topicoId, usuarioId, "deletar");

            _dataContext.Comentarios.Remove(comentario);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        private void ValidarConteudoComentario(string conteudo)
        {
            if (string.IsNullOrEmpty(conteudo))
                throw new ArgumentException("O campo 'conteudo' não pode estar vazio.");

            if (conteudo.Length > 2000)
                throw new ArgumentException("O campo 'conteudo' não pode conter mais de 2000 caracteres.");
        }

        private async Task<Comentario> ObterEValidarComentario(int comentarioId, int topicoId, int usuarioId, string texto)
        {
            var comentario = await _dataContext.Comentarios.FindAsync(comentarioId);

            if (comentario == null)
                throw new ArgumentException("O comentário com o Id especificado não existe.");

            if (comentario.TopicoId != topicoId)
                throw new ArgumentException("O ID do tópico do comentário não corresponde ao ID do tópico fornecido.");

            if (comentario.UsuarioId != usuarioId)
                throw new ArgumentException($"Você não tem permissão para {texto} este comentário.");

            return comentario;
        }
    }
}