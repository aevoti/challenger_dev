using Forum.ConcreteProduct;
using Forum.Creator;
using Forum.Data;
using Forum.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity.SqlServer;

namespace Forum.ConcreteCreator
{
    public class ComentarioFactory : ConteudoFactory
    {
        private int? _id;
        private int _idUsuario;
        private int _idTopico;
        private DateTime _data;
        private string _descricao;
        private readonly Context _context;

        public ComentarioFactory(Context context)
        {
            this._context = context;
        }
        public ComentarioFactory(int? id, int idUsuario, int _idTopico, DateTime data, string descricao, Context context)
        {
            this._id = id;
            this._idUsuario = idUsuario;
            this._idTopico = _idTopico;
            this._data = data;
            this._descricao = descricao;
            this._context = context;
        }

        public async override Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudo(int? idTopico)
        {
            IQueryable<Comentario> query = this._context.Comentario;

            query = query.AsNoTracking()
                         .OrderBy(comentario => comentario.Id)
                         .Where(comentario => comentario.IdTopico == idTopico);

            return await query.ToArrayAsync();
        }

        public async override Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudoTexto(string descricao)
        {
            IQueryable<Comentario> query = this._context.Comentario;

            query = query.AsNoTracking()
                         .OrderBy(comentario => comentario.Id)
                         .Where(comentario => comentario.Descricao.Contains(descricao));

            return await query.ToArrayAsync();
        }

        public async override Task<ActionResult<Conteudo>> BuscarConteudo(int id)
        {
            var Comentario = await _context.Comentario.FindAsync(id);

            if (Comentario == null)
            {
                return NotFound();
            }

            return Comentario;
        }

        public async override Task<IActionResult> CadastrarConteudo()
        {
            try
            {
                this._context.Add(new Comentario(null, this._idUsuario, this._idTopico, this._data, this._descricao));
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
                _context.Update(new Comentario(id, this._idUsuario, this._idTopico, this._data, this._descricao));
                var resultado = await this._context.SaveChangesAsync();
                return Ok(resultado);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificaComentario(id))
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
            var Comentario = await this._context.Comentario.FindAsync(id);
            if (Comentario != null)
            {
                _context.Comentario.Remove(Comentario);
            }
            else
            {
                return NotFound();
            }

            var resultado = await this._context.SaveChangesAsync();
            return Ok(resultado);
        }

        private bool VerificaComentario(int id)
        {
            return (this._context.Comentario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
