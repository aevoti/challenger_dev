using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {

        private readonly ForumContext _context;

        public ComentarioRepository(ForumContext context)
        {
            _context = context;
        }
        public async Task<Comentario> Create(int idTopico, Comentario comentario)
        {
            comentario.Data = DateTime.Now;
            comentario.IdTopico = idTopico;
            //comentario.IdTopicoNavigation = _context.Topico.Where(x => x.Id == idTopico).FirstOrDefault();
            //comentario.IdUsuNavigation = _context.Usuario.Where(x => x.Id == comentario.IdUsu).FirstOrDefault();
            _context.Comentario.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        public async Task<Comentario> Update(int id,int idTopico,Comentario comentario)
        {
            Comentario? cBase = _context.Comentario.FirstOrDefault(u => u.Id == id && u.IdTopico== idTopico) ;
            if (cBase == null) return new Comentario();

            _context.Entry(cBase).State = EntityState.Modified;
            cBase.Dsc = comentario.Dsc;
            _context.Comentario.Update(cBase);
            await _context.SaveChangesAsync();
            return cBase;
        }

        public async Task<bool> Delete(int idTopico, int id)
        {
            var coment = _context.Comentario.FirstOrDefault(c => c.IdTopico == idTopico && c.Id == id);
            if (coment == null) return false;

            _context.Comentario.Remove(coment);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Comentario>> Get(int idTopico)
        {
            return await _context.Comentario.Where(x => x.IdTopico == idTopico).ToListAsync();
        }
        public async Task<IEnumerable<Comentario>> Get(int idTopico,int id)
        {
            return await _context.Comentario.Where(x => x.IdTopico == idTopico && x.Id == id).ToListAsync();
        }
    }
}
