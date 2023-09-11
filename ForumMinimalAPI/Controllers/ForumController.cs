using ForumMinimalAPI.Models;
using ForumMinimalAPI.Services.ForumService;
using Microsoft.AspNetCore.Mvc;

namespace ForumMinimalAPI.Controllers
{
    [Route("api")]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpGet("forum")]
        public async Task<ActionResult<List<Topico>>> ObterTopicos([FromQuery] string filtroTexto, [FromQuery] string ordenacao)
        {
            try
            {
                var topicos = await _forumService.ObterTopicos(filtroTexto, ordenacao);
                return Ok(topicos);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpGet("topico/{id}")]
        public async Task<ActionResult<Topico>> ObterTopicoPorId(int id)
        {
            try
            {
                var topico = await _forumService.ObterTopicoPorId(id);
                return Ok(topico);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpPost("topico")]
        public async Task<ActionResult<Topico>> CadastrarTopico([FromBody] Topico topico)
        {
            try
            {
                var novoTopico = await _forumService.CadastrarTopico(topico);
                return Ok(novoTopico);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpPut("topico/{id}")]
        public async Task<ActionResult<Topico>> AtualizarTopico(int id, [FromBody] Topico topico)
        {
            try
            {
                var topicoAtualizado = await _forumService.AtualizarTopico(id, topico);
                return Ok(topicoAtualizado);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpDelete("topico/{id}")]
        public async Task<ActionResult<bool>> DeletarTopico(int id)
        {
            try
            {
                bool topicoApagado = await _forumService.DeletarTopico(id);
                return Ok(topicoApagado);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpPost("topico/curtir")]
        public async Task<ActionResult<bool>> CurtirOuDescurtirTopico([FromBody] Curtida curtida)
        {
            try
            {
                var novoTopico = await _forumService.CurtirOuDescurtirTopico(curtida);
                return Ok(novoTopico);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpPost("comentario/{topicoId}")]
        public async Task<ActionResult<Comentario>> CadastrarComentario(int topicoId, [FromBody] Comentario comentario)
        {
            try
            {
                var novoComentario = await _forumService.CadastrarComentario(topicoId, comentario);
                return Ok(novoComentario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpPut("comentario/{topicoId}/{usuarioId}")]
        public async Task<ActionResult<Comentario>> AtualizarComentario(int topicoId, int usuarioId, [FromBody] Comentario comentario)
        {
            try
            {
                var comentarioAtualizado = await _forumService.AtualizarComentario(topicoId, usuarioId, comentario);
                return Ok(comentarioAtualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }

        [HttpDelete("comentario/{topicoId}/{usuarioId}")]
        public async Task<ActionResult<bool>> DeletarComentario(int topicoId, int usuarioId, [FromBody] Comentario comentario)
        {
            try
            {
                bool comentarioApagado = await _forumService.DeletarComentario(topicoId, usuarioId, comentario);
                return Ok(comentarioApagado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }
    }
}