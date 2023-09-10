using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebAPI.Dtos;

public class ComentarioCompletoDTO
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioPhoto { get; set; }
    public int TopicoId { get; set; }

}
