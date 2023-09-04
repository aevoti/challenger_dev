using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;

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
        public async Task<ActionResult<IEnumerable<TopicoDto>>> GetTopicos()
        {
            var topicos = await _context.Topicos
                .Include(t => t.Usuario)
                .Include(t => t.Comentarios)
                .ToListAsync();

            var topicosDto = topicos.Select(t => new TopicoDto
            {
                UserId = t.UserId,
                Comentarios = t.Comentarios.Select(c => new ComentarioDto
                {
                    UserId = c.UserId,
                    Id = c.Id,
                    Msg = c.Msg,
                    TopicoId = c.TopicoId
                }).ToList(),
                Id = t.Id,
                Msg = t.Msg
            }).ToList();

            return Ok(topicosDto);
        }
    }

}
