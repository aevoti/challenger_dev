using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Repositories;

public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
{
    public ComentarioRepository(ApplicationDbContext context) : base(context){ }
}
