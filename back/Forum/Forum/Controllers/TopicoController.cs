using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.ConcreteProduct;
using Forum.Data;
using Forum.Product;
using Forum.ConcreteCreator;
using Forum.Creator;

namespace Forum.Controllers
{
    [ApiController]
    public class TopicoController : Controller
    {
        private readonly Context _context;
        public TopicoController(Context context)
        {
            _context = context;
        }

        [HttpGet("forum")]
        public async Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudo()
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(_context);

            return await conteudoFactory.BuscarTodoConteudo(null);
        }

        [HttpGet("topico/descricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudoTexto(string descricao)
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(_context);

            return await conteudoFactory.BuscarTodoConteudoTexto(descricao);
        }

        [HttpGet("topico/{id}")]
        public async Task<ActionResult<Conteudo>> BuscarConteudo(int id)
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(_context);

            return await conteudoFactory.BuscarConteudo(id);
        }

        [HttpPost("topico")]
        public async Task<IActionResult> CadastrarConteudo([Bind("IdUsuario,Data,Descricao")] Topico conteudo)
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(null, conteudo.IdUsuario, DateTime.Now, conteudo.Descricao, _context);

            return await conteudoFactory.CadastrarConteudo();

        }

        [HttpPut("topico/{id}")]
        public async Task<IActionResult> EditarConteudo(int id, [Bind("IdUsuario,Data,Descricao")] Topico conteudo)
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(null, conteudo.IdUsuario, DateTime.Now, conteudo.Descricao, _context);

            return await conteudoFactory.EditarConteudo(id);

        }

        [HttpDelete("topico/{id}")]
        public async Task<IActionResult> DeletarConteudo(int id)
        {
            ConteudoFactory conteudoFactory = new TopicoFactory(_context);

            return await conteudoFactory.DeletarConteudo(id);

        }
    }
}
