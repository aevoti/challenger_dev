using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
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
}
