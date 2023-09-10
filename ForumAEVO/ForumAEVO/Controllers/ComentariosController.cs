using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

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

 
        // POST: api/comentarios/{idPost}
        [HttpPost("{idTopico}")]
        [ProducesResponseType(typeof(Comentario), 200)]
        public async Task<ActionResult<Comentario>> PostComentario([FromBody] MsgUpdateDTO comentarioDto,int idTopico)
        {
            if (comentarioDto == null)
            {
                return BadRequest("O objeto de comentário não pode ser nulo.");
            }
            var comentario = new Comentario
            {
                UserId = comentarioDto.UserId,
                Data = DateTime.Now.Date,
                TopicoId = idTopico,
                Msg = comentarioDto.Msg

            };
           

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentario", new { id = comentario.Id }, comentario);
        }

        //PUT: api/comentarios/{idTopico}/{id}
        [HttpPut("{idTopico}/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutComentario(int idTopico, int id, [FromBody] MsgUpdateDTO comentarioUpdtade)
        {
             GetAndValidateToken();
            var topico = await _context.Topicos.FindAsync(idTopico);
            if (topico == null)
            {
               return NotFound("IdTopico não encontrado.");
            }


            // Busca se o usuário "autenticado" é dono do comentário
            var comentario = await _context.Comentarios
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id && c.TopicoId == idTopico && c.UserId == comentarioUpdtade.UserId);

            if (comentario == null)
            {
                return Unauthorized("Você não tem permissão para editar este comentário.");
            }

            // Atualiza o comentário
            comentario.Msg = comentarioUpdtade.Msg;

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

            return NoContent();
        }

        // DELETE: api/comentarios/{idTopico}/{id}
        [HttpDelete("{idTopico}/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteComentario(int idTopico, int id)
        {
            var userId = GetAndValidateToken();

            var topico = await _context.Topicos.FindAsync(idTopico);
            var donoComentario = await _context.Comentarios.FindAsync(id);

            if (topico == null || donoComentario==null)
            {
                return NotFound(new { message = "Topico Ou Coméntario não encontrado." });
            }
            
            if(donoComentario.UserId != userId)
            {
                return Unauthorized("Você não tem permissão para excluir este comentário.");
            }

            // Busca se o usuário "autenticado" é dono do comentário
            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(c => c.Id == id && c.TopicoId == idTopico && c.UserId == userId);

            if (comentario == null)
            {
                return NotFound(new { message = "Comentário não encontrado." });
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.Id == id);
        }

        //Função para verificar o Token fake do Header
        private Guid GetAndValidateToken()
        {
            // Acessa o Token do cabeçalho da solicitação, o fakeToken é o Id da classe Usuário
            var fakeToken = HttpContext.Request.Headers["Token"].ToString();

            // Verifica se o Token do tipo Guid válido se sim ele o envia para a variavel token
            if (!Guid.TryParse(fakeToken, out var token))
            {

                throw new ArgumentException("Token inválido.");
            }

            return token;
        }

    }
}
