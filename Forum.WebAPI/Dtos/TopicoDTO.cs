using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Dtos;

public class TopicoDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int UsuarioId { get; set; }

    public static TopicoDTO MapToDTO(Topico topico)
    {
        return new TopicoDTO
        {
            Id = topico.Id,
            Titulo = topico.Titulo,
            Descricao = topico.Descricao,
            UsuarioId = topico.UsuarioId,
        };
    }
}
