using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Repositories;
 
public interface IComentarioRepository : IRepository<Comentario>
{
    Comentario? GetComentario(int idTopico, int idComentario);
}
