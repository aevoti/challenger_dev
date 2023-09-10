using Forum.WebAPI.Dtos;
using Forum.WebAPI.Models;
using Forum.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly IComentarioRepository _comentarioRepository;
    private readonly ITopicoRepository _topicoRepository;

    public ComentarioController(IComentarioRepository comentarioRepository, ITopicoRepository topicoRepository)
    {
        _comentarioRepository = comentarioRepository;
        _topicoRepository = topicoRepository;
    }

    // /comentario/{idTopico} - [POST] - Deve cadastrar um novo comentario no topico de id especificado
    [HttpPost("comentario/{idTopico}")]
    public IActionResult Post(int idTopico, ComentarioDTO comentarioDTO)
    {
        var topico = _topicoRepository.GetTopicoById(idTopico);
        if (topico == null)
        {
            return NotFound("Tópico não encontrado.");
        }

        if (comentarioDTO == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var novoComentario = new Comentario
        {
            Descricao = comentarioDTO.Descricao,
            UsuarioId = comentarioDTO.UsuarioId,
            TopicoId = idTopico
        };

        _comentarioRepository.Add(novoComentario);
        if(_comentarioRepository.SaveChanges())
        {
            return CreatedAtAction(nameof(Post), new { idTopico = novoComentario.TopicoId, id = novoComentario.Id }, novoComentario);
        }
        
        return BadRequest("Comentário não cadastrado");
    }

    // comentario/{idTopico}/{id} - [PUT] - Deve atualizar um comentario com o id especificado 
    [HttpPut("comentario/{idTopico}/{idComentario}")]
    public IActionResult Put(int idTopico, int idComentario, ComentarioDTO comentarioAtualizado)
    {
        var topico = _topicoRepository.GetTopicoById(idTopico);

        if (topico == null)
        {
            return NotFound("Tópico não encontrado.");
        }

        var comentarioExistente = _comentarioRepository.GetComentario(idTopico, idComentario);

        if (comentarioExistente == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        comentarioExistente.Descricao = comentarioAtualizado.Descricao;
        _comentarioRepository.Update(comentarioExistente);
        if(_topicoRepository.SaveChanges())
        {
            return Ok();
        }

        return BadRequest("Comentário não atualizado");
    }

    // /comentario/{idTopico}/{id} - [DELETE] - Deve deletar um comentario com o id especificado 
    [HttpDelete("comentario/{idTopico}/{idComentario}")]
    public IActionResult Delete(int idTopico, int idComentario)
    {
        var topico = _topicoRepository.GetTopicoById(idTopico);
        if (topico == null)
        {
            return NotFound("Tópico não encontrado.");
        }

        var comentario = _comentarioRepository.GetComentario(idTopico, idComentario);

        if (comentario == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        _comentarioRepository.Remove(comentario);
        if(_comentarioRepository.SaveChanges())
        {
            return Ok();
        }

        return BadRequest("Comentário não deletado");
    }
}
