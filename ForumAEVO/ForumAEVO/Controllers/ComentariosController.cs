using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;

namespace ForumAEVO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly Context _context;

        public ComentariosController(Context context)
        {
            _context = context;
        }

        //GET: api/comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentarios()
        {
            var comentarios = await _context.Comentarios.ToListAsync();

            var comentariosDto = comentarios.Select(comentario => new ComentarioDto
            {
                TopicoId = comentario.TopicoId,
                UserId = comentario.UserId,
                Id = comentario.Id,
                Msg = comentario.Msg,
                Data = comentario.Data
            }).ToList();

            return Ok(comentariosDto);
        }


        // POST: api/comentarios
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario([FromBody] Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest("O objeto de comentário não pode ser nulo.");
            }

            comentario.Id = Guid.NewGuid();
            comentario.Data = DateTime.Now.Date; // Define a data atual sem a hora

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentario", new { id = comentario.Id }, comentario);
        }

        // PUT: api/comentarios/{idTopico}/{id}
        [HttpPut("{idTopico}/{id}")]
        public async Task<IActionResult> PutComentario(Guid idTopico, Guid id, [FromBody] ComentarioDto comentarioDto)
        {
            var comentario = await _context.Comentarios
                .Include(c => c.Usuario) 
                .FirstOrDefaultAsync(c => c.Id == id && c.TopicoId == idTopico);

            if (comentario == null)
            {
                return NotFound();
            }

            // Atualizando apenas o campo mensagem quem vem do JSON
            comentario.Msg = comentarioDto.Msg;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(comentarioDto);
        }



        // DELETE: api/comentarios/{idTopico}/{id}
        [HttpDelete("{idTopico}/{id}")]
        public async Task<IActionResult> DeleteComentario(Guid idTopico, Guid id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            
            if (comentario == null)
            {
                return NotFound(new { message = "Comentário não encontrado." });
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ComentarioExists(Guid id)
        {
            return _context.Comentarios.Any(e => e.Id == id);
        }
    }
}
