using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;

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

        // GET: api/topicos/5 DEPOIS tratar se o Id nao existir
        [HttpGet("{id}")]
        public async Task<ActionResult<Topico>> GetTopico(Guid id)
        {
            var topico = await _context.Topicos
                .Include(t => t.Usuario) 
                .Include(t => t.Comentarios) 
                .ThenInclude(c => c.Usuario) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (topico == null)
            {
                return NotFound();
            }

            return topico;
        }


        // POST: api/topicos
        [HttpPost]
        public async Task<ActionResult<Topico>> PostTopico([FromBody] Topico topico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            topico.Id = Guid.NewGuid();
            topico.Data = DateTime.Now.Date; // informando apenas a data no formato dd/mm/aaaa

            // Inclui o usuário associado ao tópico
            topico.Usuario = await _context.Usuarios.FindAsync(topico.UserId);

            _context.Topicos.Add(topico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopico", new { id = topico.Id }, topico);
        }

        // PUT: api/topicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopico(Guid id, [FromBody] MsgUpdateDTO topicoMsgUpdateDTO)
        {
            if (topicoMsgUpdateDTO == null)
            {
                return BadRequest("Dados de atualização inválidos.");
            }

            var topico = await _context.Topicos.FindAsync(id);

            if (topico == null)
            {
                return NotFound("Tópico não encontrado.");
            }

            // Atualiza apenas o campo de mensagem do tópico com o valor fornecido no DTO.
            topico.Msg = topicoMsgUpdateDTO.Msg;

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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/topicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopico(Guid id)
        {
            var topico = await _context.Topicos.FindAsync(id);
            if (topico == null)
            {
                return NotFound();
            }

            // Excluir os comentários associados a este tópico
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
