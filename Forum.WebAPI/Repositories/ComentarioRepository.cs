using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;
using Forum.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.WebAPI.Repositories;

public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
{
    public ComentarioRepository(ApplicationDbContext context) : base(context){ }

    public Comentario? GetComentario(int idTopico, int idComentario)
    {
        var comentario = _context.Set<Comentario>()
                        .Where(c => c.Id == idComentario && c.TopicoId == idTopico)
                        .FirstOrDefault();

        if (comentario == null)
        {
            return null;
        }

        return comentario;
    }
}
