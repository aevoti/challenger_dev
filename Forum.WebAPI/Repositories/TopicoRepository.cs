using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
using Forum.WebAPI.Enums;
using Microsoft.EntityFrameworkCore;
using Forum.WebAPI.Dtos;

namespace Forum.WebAPI.Repositories;

public class TopicoRepository : Repository<Topico>, ITopicoRepository
{
    public TopicoRepository(ApplicationDbContext context) : base(context){ }

    public TopicoCompletoDTO? GetTopicoById(int id)
    {
        var query = _context.Topicos
            .Include(t => t.Usuario) 
            .Include(t => t.Comentarios)
            .FirstOrDefault(t => t.Id == id);

        if(query != null){
            var topicosDto = new TopicoCompletoDTO
            {
                Id = query.Id,
                Titulo = query.Titulo,
                Descricao = query.Descricao,
                DataCriacao = query.DataCriacao,
                UsuarioId = query.UsuarioId,
                UsuarioName = query.Usuario.Name,
                UsuarioPhoto = query.Usuario.Photo,
                Comentarios = query.Comentarios.Select(c => new ComentarioCompletoDTO
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    UsuarioId = c.UsuarioId,
                    UsuarioPhoto = c.Usuario.Photo,
                    TopicoId = c.TopicoId
                }).ToList()
            };
            return topicosDto;
        }
        return null;
    }

    public Topico? GetTopicoObjById(int id)
    {
        return _context.Set<Topico>()
                    .Include(t => t.Usuario) 
                    .Include(t => t.Comentarios)
                    .FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Topico>? GetAllTopicos()
    {
        return _context.Set<Topico>()
                        .Include(t => t.Usuario) 
                        .Include(t => t.Comentarios)
                        .ToList();
    }

    public IEnumerable<TopicoCompletoDTO> GetTopicosOrdenadosEOuPesquisados(TipoDeOrdenacao ordenacao, string searchText)
    {
        var query = _context.Topicos
            .Include(t => t.Usuario) 
            .Include(t => t.Comentarios)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(t =>
                EF.Functions.Like(t.Titulo, $"%{searchText}%") ||
                EF.Functions.Like(t.Descricao, $"%{searchText}%"));
        }

        switch (ordenacao)
        {
            case TipoDeOrdenacao.Crescente:
                query = query.OrderBy(t => t.DataCriacao);
                break;
            case TipoDeOrdenacao.Decrescente:
                query = query.OrderByDescending(t => t.DataCriacao);
                break;
        }

        var topicosDto = query.Select(t => new TopicoCompletoDTO
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Descricao = t.Descricao,
            DataCriacao = t.DataCriacao,
            UsuarioId = t.UsuarioId,
            UsuarioName = t.Usuario.Name,
            UsuarioPhoto = t.Usuario.Photo,
            Comentarios = t.Comentarios.Select(c => new ComentarioCompletoDTO
            {
                Id = c.Id,
                Descricao = c.Descricao,
                UsuarioId = c.UsuarioId,
                UsuarioPhoto = c.Usuario.Photo,
                TopicoId = c.TopicoId
            }).ToList()
        }).ToList();

        return topicosDto;
    }
}
