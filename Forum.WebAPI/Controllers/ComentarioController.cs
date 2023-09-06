using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ComentarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ComentarioController(ApplicationDbContext context) 
    { 
        _context = context;
    }

    // /comentario/{idTopico} - [POST] - Deve cadastrar um novo comentario no topico de id especificado
    [HttpPost("{idTopico}")]
    public async Task<ActionResult<Comentario>> Post(int idTopico, Comentario comentario)
    {
        var topico = _context.Topicos.Find(idTopico);

        if (topico == null)
        {
            return NotFound("Tópico não encontrado.");
        }

        // Defina a relação entre o comentário e o tópico
        comentario.TopicoId = idTopico;

        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { idTopico, id = comentario.Id }, comentario);
    }

    // comentario/{idTopico}/{id} - [PUT] - Deve atualizar um comentario com o id especificado 
    [HttpPut("{idTopico}/{id}")]
    public async Task<IActionResult> Put(int idTopico, int id, Comentario comentarioAtualizado)
    {
        if (id != comentarioAtualizado.Id)
        {
            return BadRequest("Dados inválidos.");
        }

        var comentarioExistente = _context.Comentarios.Find(id);

        if (comentarioExistente == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        if (comentarioExistente.TopicoId != idTopico)
        {
            return BadRequest("Este comentário não pertence a este tópico.");
        }

        // Atualize as propriedades do comentário existente com base no comentário atualizado
        comentarioExistente.Descricao = comentarioAtualizado.Descricao;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // /comentario/{idTopico}/{id} - [DELETE] - Deve deletar um comentario com o id especificado 
    [HttpDelete("{idTopico}/{id}")]
    public async Task<IActionResult> Delete(int idTopico, int id)
    {
        var comentario = await _context.Comentarios.FindAsync(id);

        if (comentario == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        if (comentario.TopicoId != idTopico)
        {
            return BadRequest("Este comentário não pertence a este tópico.");
        }

        _context.Comentarios.Remove(comentario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // /comentario/{idTopico}/{id} - [GET] - Deve retornar um comentario com o id especificado
    [HttpGet("{idTopico}/{id}")]
    public async Task<ActionResult<Comentario>> Get(int idTopico, int id)
    {
        var comentario = await _context.Comentarios.FindAsync(id);

        if (comentario == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        if (comentario.TopicoId != idTopico)
        {
            return BadRequest("Este comentário não pertence a este tópico.");
        }

        return Ok(comentario);
    }
}
