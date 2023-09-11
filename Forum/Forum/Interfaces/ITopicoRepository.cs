using Microsoft.AspNetCore.Mvc;
using Models;
namespace Interfaces
{
    public interface ITopicoRepository 
    {
        Task<Topico> GetById(int id);
        Task<IEnumerable<Topico>> Get(int ordenacao);
        Task<IEnumerable<Topico>> Pesquisa(string textoPesquisa,int ordenacao);
        Task<int> Create(Topico topico);
        Task<Topico> Update(int id,Topico topico);
        Task<bool> Delete(int id);

    }
}
