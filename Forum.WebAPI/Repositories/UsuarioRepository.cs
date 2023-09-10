using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context){ }

    public Usuario? GetUsuarioById(int id)
    {
        return _context.Set<Usuario>().Find(id);
    }
}
