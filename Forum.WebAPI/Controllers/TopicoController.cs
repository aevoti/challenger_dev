using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
public class TopicoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TopicoController(ApplicationDbContext context) 
    { 
        _context = context;
    }

    // /forum - [GET] - Deve Retornar todos os topicos enviados
    [HttpGet("forum")]
    public IActionResult GetForumTopics()
    {
        var topicos = _context.Topicos.ToList();
        return Ok(topicos);
    }

    // /topico/{id} - [GET] - Deve retornar um topico com id especificado
    [HttpGet("topico/{id}")]
    public IActionResult Get(int id)
    {
        var topico = _context.Topicos.Find(id);

        if (topico == null)
        {
            return NotFound();
        }

        return Ok(topico);
    }

    // /topico - [POST] - Deve cadastrar um novo topico
    [HttpPost("topico")]
    public IActionResult Post([FromBody] Topico topico)
    {
        if (topico == null)
        {
            return BadRequest("Dados inválidos.");
        }

        _context.Topicos.Add(topico);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = topico.Id }, topico);
    }

    // /topico/{id} - [PUT] - Deve atualizar um topico com o id especificado
    [HttpPut("topico/{id}")]
    public IActionResult Put(int id, [FromBody] Topico topicoAtualizado)
    {
        _context.Topicos.Add(topicoAtualizado);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = topicoAtualizado.Id }, topicoAtualizado);
    }

    // /topico/{id} - [DELETE] - Deve deletar um topico com o id especificado
    [HttpDelete("topico/{id}")]
    public IActionResult Delete(int id)
    {
        var topico = _context.Topicos.Find(id);

        if (topico == null)
        {
            return NotFound();
        }

        _context.Topicos.Remove(topico);
        _context.SaveChanges();

        return NoContent();
    }
}
