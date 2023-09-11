using Microsoft.EntityFrameworkCore;
using Models;
using Interfaces;
using Microsoft.Identity.Client;

namespace Repositories
{
    public class TopicoRepository : ITopicoRepository
    {
        private readonly ForumContext _context;

        public TopicoRepository(ForumContext context) {
            _context = context;
        }
        public async Task<int> Create(Topico topico)
        {
            _context.Topico.Add(topico);
            await SaveAllAsync();
            return 0;
        }
        public async Task<IEnumerable<Topico>> Get(int ordenacao)
        {
            return await ((ordenacao == 0) ? _context.Topico.OrderByDescending(t => t.Data)
                                           : _context.Topico.OrderBy(t => t.Data))
                         .ThenBy(t => t.Id)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Topico>> Pesquisa(string textoPesquisa,int ordenacao)
        {
            return await ((ordenacao == 0) ? _context.Topico.OrderByDescending(t => t.Data)
                                           : _context.Topico.OrderBy(t => t.Data))
                          .ThenBy(t => t.Id)
                          .Where(t => (t.Dsc.Contains(textoPesquisa) || textoPesquisa==""))
                          .ToListAsync();
        }

        public async Task<Topico> GetById(int id)
        {
            var topic = await _context.Topico.FirstOrDefaultAsync(t => t.Id == id);
            return (topic == null ? new Topico() : topic);
        }
        public async Task<Topico> Update(int id, Topico topico)
        {

            Topico? tBase = _context.Topico.FirstOrDefault(t => t.Id == id);
            if (tBase == null) return new Topico();
            _context.Entry(tBase).State = EntityState.Modified;
            tBase.Dsc = topico.Dsc;
            _context.Topico.Update(tBase);
            await SaveAllAsync();
            return topico;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                _context.Topico.Remove(await GetById(id));
                await SaveAllAsync();
                return true;
            }
            catch (Exception){throw;}
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}