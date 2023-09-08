using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
using Forum.WebAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace Forum.WebAPI.Repositories;

public class TopicoRepository : Repository<Topico>, ITopicoRepository
{
    public TopicoRepository(ApplicationDbContext context) : base(context){ }

    public Topico? GetTopicoById(int id)
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

    public IEnumerable<Topico> GetTopicosOrdenadosEOuPesquisados(TipoDeOrdenacao ordenacao, string searchText)
    {
        IQueryable<Topico> query = _context.Topicos;

        switch (ordenacao)
        {
            case TipoDeOrdenacao.Crescente:
                query = query.OrderBy(t => t.DataCriacao);
                break;
            case TipoDeOrdenacao.Decrescente:
                query = query.OrderByDescending(t => t.DataCriacao);
                break;
        }

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(t =>
                EF.Functions.Like(t.Titulo, $"%{searchText}%") ||
                EF.Functions.Like(t.Descricao, $"%{searchText}%"));
        }

        return query.ToList();
    }
}
