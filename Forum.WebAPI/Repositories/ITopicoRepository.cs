using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Dtos;
using Forum.WebAPI.Enums;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Repositories;

public interface ITopicoRepository : IRepository<Topico>
{
    TopicoCompletoDTO? GetTopicoById(int id);
    
    Topico? GetTopicoObjById(int id);

    IEnumerable<Topico>? GetAllTopicos();

    IEnumerable<TopicoCompletoDTO> GetTopicosOrdenadosEOuPesquisados(TipoDeOrdenacao ordenacao, string searchText);

}
