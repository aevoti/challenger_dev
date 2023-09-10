using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebAPI.Dtos;

public class TopicoCompletoDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioName { get; set; }
    public string UsuarioPhoto { get; set; }
    public List<ComentarioCompletoDTO> Comentarios { get; set; }
}
