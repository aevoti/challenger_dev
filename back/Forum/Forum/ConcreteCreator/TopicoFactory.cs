using Forum.ConcreteProduct;
using Forum.Creator;
using Forum.Data;
using Forum.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.SqlServer;

namespace Forum.ConcreteCreator
{
    public class TopicoFactory : ConteudoFactory
    {
        private int? _id;
        private int _idUsuario;
        private DateTime _data;
        private string _descricao;
        private readonly Context _context;

        public TopicoFactory(Context context)
        {
            this._context = context;
        }
        public TopicoFactory(int? id, int idUsuario, DateTime data, string descricao, Context context)
        {
            this._id = id;
            this._idUsuario = idUsuario;
            this._data = data;
            this._descricao = descricao;
            this._context = context;
        }

        public async override Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudo(int? idTopico)
        {
            return await this._context.Topico.ToListAsync();
        }

        public async override Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudoTexto(string descricao)
        {
            IQueryable<Topico> query = this._context.Topico;

            query = query.AsNoTracking()
                         .OrderBy(topico => topico.Id)
                         .Where(topico => topico.Descricao.Contains(descricao));

            return await query.ToArrayAsync();
        }

        public async override Task<ActionResult<Conteudo>> BuscarConteudo(int id)
        {
            var topico = await _context.Topico.FindAsync(id);

            if (topico == null)
            {
                return NotFound();
            }

            return topico;
        }

        public async override Task<IActionResult> CadastrarConteudo()
        {
            try
            {
                this._context.Add(new Topico(null, this._idUsuario, this._data, this._descricao));
                var resultado = await this._context.SaveChangesAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        public async override Task<IActionResult> EditarConteudo(int id)
        {
            try
            {
                _context.Update(new Topico(id, this._idUsuario, this._data, this._descricao));
                var resultado = await this._context.SaveChangesAsync();
                return Ok(resultado);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificaTopico(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public async override Task<IActionResult> DeletarConteudo(int id)
        {
            var topico = await this._context.Topico.FindAsync(id);
            if (topico != null)
            {
                _context.Topico.Remove(topico);
            }
            else
            {
                return NotFound();
            }

            var resultado = await this._context.SaveChangesAsync();
            return Ok(resultado);
        }

        private bool VerificaTopico(int id)
        {
            return (this._context.Topico?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
