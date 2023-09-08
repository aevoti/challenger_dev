using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace ForumAEVO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicosController : ControllerBase
    {
        private readonly Context _context;

        public TopicosController(Context context)
        {
            _context = context;
        }

        // GET: api/topicos/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Topico>> GetTopico(Guid id)
        {
            var topico = await _context.Topicos
                   .Include(t => t.Comentarios)
                   .FirstOrDefaultAsync(t => t.Id == id);

            if (topico == null)
            {
                return NotFound("Não existe tópico com este ID.");
            }

            // Confingurando saida do objeto para a API
            var resultado = new
            {
                userId = topico.UserId.ToString(),
                id = topico.Id.ToString(),
                msg = topico.Msg,
                data = topico.Data.ToString("dd-MM-yyyy"),
                comentarios = topico.Comentarios.Select(c => new
                {
                    userId = c.UserId.ToString(),
                    id = c.Id.ToString(),
                    msg = c.Msg,
                    data = c.Data.ToString("dd-MM-yyyy")
                }).ToList()
            };

            return Ok(resultado);
        }

        // POST: api/topicos
        [HttpPost] 
        public async Task<ActionResult<Topico>> PostTopico([FromBody] TopicoDto topicodto)
        {
            var topico = new Topico();
            
            if (topicodto == null)
            {
                return BadRequest("Dados de atualização inválidos.");
            }

            topico.Id = Guid.NewGuid();
            topico.Data = DateTime.Now.Date;
            topico.UserId = topicodto.UserId;
            topico.Msg = topicodto.Msg;

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopico", new { id = topico.Id }, topico);
        }

        // PUT: api/topicos/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MsgUpdateDTO), 204)]
        public async Task<IActionResult> PutTopico(Guid id, [FromBody] MsgUpdateDTO topicoMsgUpdate)
        {   

            if (topicoMsgUpdate == null)
            {
                return BadRequest("Dados de atualização inválidos.");
            }

            var topico = await _context.Topicos.FindAsync(id);

            if (topico == null)
            {
                return NotFound("Tópico não encontrado.");
            }

            var fakeToken= HttpContext.Request.Headers["Token"].ToString();
            if (!Guid.TryParse(fakeToken, out var userId))
            {
                return BadRequest("Headers Token inválido.");
            }

            if (topico.UserId != userId)
            {
                return Unauthorized("Você não tem permissão para editar este tópico.");
            }

            // Atualizando o comentário
            topico.Msg = topicoMsgUpdate.Msg;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicoExists(id))
                {
                    return NotFound("Tópico não encontrado.");
                }
                else //se houver exceção a biblioteca lançará
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/topicos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopico(Guid id)
        {
            var topico = await _context.Topicos.FindAsync(id);
            if (topico == null)
            {
                return NotFound("O Tópico procurado não existe.");
            }

            var userIdHeader = HttpContext.Request.Headers["Token"].ToString();
            if (!Guid.TryParse(userIdHeader, out var userId))
            {
                return BadRequest("UserId inválido.");
            }

            if (topico.UserId != userId)
            {
                return Unauthorized("Você não tem permissão para excluir este tópico.");
            }

            var comentarios = _context.Comentarios.Where(c => c.TopicoId == id);
            _context.Comentarios.RemoveRange(comentarios);

            // excluindo o tópico
            _context.Topicos.Remove(topico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

            private bool TopicoExists(Guid id)
        {
            return _context.Topicos.Any(e => e.Id == id);
        }
    }
}
