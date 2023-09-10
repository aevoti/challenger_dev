using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace ForumAEVO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForumController : ControllerBase
    {
        private readonly Context _context;

        public ForumController(Context context)
        {
            _context = context;
        }


        // GET: api/forum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumDTO>>> GetTopicos()
        {
            var topicos = await _context.Topicos
                .Include(t => t.Usuario)
                .Include(t => t.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .ToListAsync();

            //formantando para a saida do GEt
            var topicosDto = topicos.Select(t => new ForumDTO
            {
                UserId = t.UserId,
                Comentarios = t.Comentarios.Select(c => new ComentarioDto
                {
                    UserId = c.UserId,
                    Id = c.Id,
                    DonoDaPostagem = c.Usuario != null ? c.Usuario.Nome : "Nome não especificado",//para fazer o compilador para de reclamar usuário e foto
                    Foto = c.Usuario != null ? c.Usuario.Foto : "https://material.angular.io/assets/img/examples/shiba2.jpg",// nunca serão nulos na criação trato isso.
                    Msg = c.Msg,
                    TopicoId = c.TopicoId,
                    Data = t.Data.ToString("dd-MM-yyyy")
                }).ToList(),
                Id = t.Id,
                DonoDaPostagem = t.Usuario != null ? t.Usuario.Nome : "Nome não especificado",
                Foto = t.Usuario != null ? t.Usuario.Foto : "https://material.angular.io/assets/img/examples/shiba2.jpg",//Url do site do angular shiba inu
                Msg = t.Msg,
                Data = t.Data.ToString("dd-MM-yyyy")
            }).ToList();

            return Ok(topicosDto);
        }

    }

}
