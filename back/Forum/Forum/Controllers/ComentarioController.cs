using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.ConcreteProduct;
using Forum.Data;
using Forum.ConcreteCreator;
using Forum.Creator;
using Forum.Product;

namespace Forum.Controllers
{
    [ApiController]
    public class ComentarioController : Controller
    {
        private readonly Context _context;
        public ComentarioController(Context context)
        {
            _context = context;
        }

        [HttpGet("comentario/topico/{idTopico}")]
        public async Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudo(int idTopico)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(_context);

            return await conteudoFactory.BuscarTodoConteudo(idTopico);
        }

        [HttpGet("comentario/descricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudoTexto(string descricao)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(_context);

            return await conteudoFactory.BuscarTodoConteudoTexto(descricao);
        }


        [HttpPost("comentario/{idTopico}")]
        public async Task<IActionResult> CadastrarConteudo(int idTopico, [Bind("IdUsuario,Data,Descricao")] Comentario conteudo)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(null, conteudo.IdUsuario, idTopico, DateTime.Now, conteudo.Descricao, _context);

            return await conteudoFactory.CadastrarConteudo();

        }

        [HttpGet("comentario/{id}")]
        public async Task<ActionResult<Conteudo>> BuscarConteudo(int id)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(_context);

            return await conteudoFactory.BuscarConteudo(id);
        }

        [HttpPut("comentario/{id}")]
        public async Task<IActionResult> EditarConteudo(int id, [Bind("IdUsuario,Data,Descricao")] Comentario conteudo)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(null, conteudo.IdUsuario, conteudo.IdTopico, DateTime.Now, conteudo.Descricao, _context);

            return await conteudoFactory.EditarConteudo(id);

        }

        [HttpDelete("Comentario/{id}")]
        public async Task<IActionResult> DeletarConteudo(int id)
        {
            ConteudoFactory conteudoFactory = new ComentarioFactory(_context);

            return await conteudoFactory.DeletarConteudo(id);

        }
    }
}
