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
        public async Task<ActionResult<TopicoDto>> GetTopico(int id)
        {
            var topico = await _context.Topicos
                .Include(t => t.Usuario)
                .Include(t => t.Comentarios)
                .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topico == null)
            {
                return NotFound("Não existe tópico com este ID.");
            }

            // Configurando saída do objeto para a API
            var topicoDto = new ForumDTO
            {
                UserId = topico.UserId,
                Comentarios = topico.Comentarios.Select(c => new ComentarioDto
                {
                    UserId = c.UserId,
                    Id = c.Id,
                    DonoDaPostagem = c.Usuario != null ? c.Usuario.Nome : "Nome não especificado",
                    Foto = c.Usuario != null ? c.Usuario.Foto : "https://material.angular.io/assets/img/examples/shiba2.jpg",
                    Msg = c.Msg,
                    TopicoId = c.TopicoId,
                    Data = c.Data.ToString("dd-MM-yyyy")
                }).ToList(),
                Id = topico.Id,
                DonoDaPostagem = topico.Usuario != null ? topico.Usuario.Nome : "Nome não especificado",
                Foto = topico.Usuario != null ? topico.Usuario.Foto : "https://material.angular.io/assets/img/examples/shiba2.jpg",
                Msg = topico.Msg,
                Data = topico.Data.ToString("dd-MM-yyyy")
            };

            return Ok(topicoDto);
        }


        // POST: api/topicos
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Topico>> PostTopico([FromBody] TopicoDto topicodto)
        {
            var topico = new Topico();
            
            if (topicodto == null)
            {
                return BadRequest("Dados de atualização inválidos.");
            }

            topico.Data = DateTime.Now.Date;
            topico.UserId = topicodto.UserId;
            topico.Msg = topicodto.Msg;

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopico", new { id = topico.Id }, topico);
        }

        // PUT: api/topicos/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutTopico(int id, [FromBody] MsgUpdateDTO topicoMsgUpdate)
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
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteTopico(int id)
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

            private bool TopicoExists(int id)
        {
            return _context.Topicos.Any(e => e.Id == id);
        }
    }
}
