using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Models;

namespace Forum.WebAPI.Dtos;

public class ComentarioDTO
{
    public string? Descricao { get; set; }
    public int UsuarioId { get; set; }
    public int TopicoId { get; set; }

    public static ComentarioDTO MapToDTO(Comentario comentario)
    {
        return new ComentarioDTO
        {
            Descricao = comentario.Descricao,
            UsuarioId = comentario.UsuarioId,
            TopicoId = comentario.TopicoId
        };
    }
}
