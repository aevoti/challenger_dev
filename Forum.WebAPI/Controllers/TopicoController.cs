using Forum.WebAPI.Dtos;
using Forum.WebAPI.Enums;
using Forum.WebAPI.Models;
using Forum.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
public class TopicoController : ControllerBase
{
    private readonly ITopicoRepository _topicoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TopicoController(ITopicoRepository topicoRepository, IUsuarioRepository usuarioRepository)
    {
        _topicoRepository = topicoRepository;
        _usuarioRepository = usuarioRepository;
    }

    // /forum - [GET] - Deve Retornar todos os topicos enviados
    [HttpGet("forum")]
    public IActionResult GetForumTopics(TipoDeOrdenacao? order = null, string searchText = "")
    {
        TipoDeOrdenacao valorOrdenacao = order ?? TipoDeOrdenacao.Crescente;
        var topicos = _topicoRepository.GetTopicosOrdenadosEOuPesquisados(valorOrdenacao, searchText);
        return Ok(topicos);
    }

    // /topico/{id} - [GET] - Deve retornar um topico com id especificado
    [HttpGet("topico/{id}")]
    public IActionResult Get(int id)
    {
        var topico = _topicoRepository.GetTopicoById(id);

        if (topico == null)
        {
            return NotFound();
        }

        return Ok(topico);
    }

    // /topico - [POST] - Deve cadastrar um novo topico
    [HttpPost("topico")]
    public IActionResult Post([FromBody] TopicoDTO topicoDTO)
    {
        if (topicoDTO == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var usuarioExistente = _usuarioRepository.GetUsuarioById(topicoDTO.UsuarioId);

        if (usuarioExistente == null)
        {
            return BadRequest("O usuário especificado não existe");
        }

        var novoTopico = new Topico
        {
            Titulo = topicoDTO.Titulo,
            Descricao = topicoDTO.Descricao,
            UsuarioId = topicoDTO.UsuarioId,
            DataCriacao =  DateTime.Now
        };

        _topicoRepository.Add(novoTopico);
        if(_topicoRepository.SaveChanges())
        {
            return CreatedAtAction(nameof(Get), new { id = novoTopico.Id }, novoTopico);
        }
        
        return BadRequest("Topico não cadastrado");
    }

    // /topico/{id} - [PUT] - Deve atualizar um topico com o id especificado
    [HttpPut("topico/{id}")]
    public IActionResult Put(int id, [FromBody] TopicoDTO topicoAtualizado)
    {
        var topicoExistente = _topicoRepository.GetTopicoObjById(id);

        if (topicoExistente == null)
        {
            return NotFound();
        }

        topicoExistente.Titulo = topicoAtualizado.Titulo;
        topicoExistente.Descricao = topicoAtualizado.Descricao;

        _topicoRepository.Update(topicoExistente);
        if(_topicoRepository.SaveChanges())
        {
            return Ok();
        }

        return BadRequest("Topico não atualizado");
    }

    // /topico/{id} - [DELETE] - Deve deletar um topico com o id especificado
    [HttpDelete("topico/{id}")]
    public IActionResult Delete(int id)
    {
        var topico = _topicoRepository.GetTopicoObjById(id);

        if (topico == null)
        {
            return NotFound();
        }

        _topicoRepository.Remove(topico);
        if(_topicoRepository.SaveChanges())
        {
            return Ok();
        }

        return BadRequest("Topico não deletado");
    }
}
