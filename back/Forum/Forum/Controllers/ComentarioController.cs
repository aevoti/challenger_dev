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

    // GET: Comentario/Details/5
    /*public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Comentario == null)
        {
            return NotFound();
        }

        var comentario = await _context.Comentario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comentario == null)
        {
            return NotFound();
        }

        return View(comentario);
    }

    // GET: Comentario/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Comentario/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdTopico,Data,Descricao")] Comentario comentario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comentario);
    }

    // GET: Comentario/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Comentario == null)
        {
            return NotFound();
        }

        var comentario = await _context.Comentario.FindAsync(id);
        if (comentario == null)
        {
            return NotFound();
        }
        return View(comentario);
    }

    // POST: Comentario/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdTopico,Data,Descricao")] Comentario comentario)
    {
        if (id != comentario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(comentario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(comentario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(comentario);
    }

    // GET: Comentario/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Comentario == null)
        {
            return NotFound();
        }

        var comentario = await _context.Comentario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comentario == null)
        {
            return NotFound();
        }

        return View(comentario);
    }

    // POST: Comentario/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Comentario == null)
        {
            return Problem("Entity set 'Context.Comentario'  is null.");
        }
        var comentario = await _context.Comentario.FindAsync(id);
        if (comentario != null)
        {
            _context.Comentario.Remove(comentario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ComentarioExists(int id)
    {
      return (_context.Comentario?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}*/
}
