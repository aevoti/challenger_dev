using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Usuario? GetUsuarioById(int id);
}
