using Microsoft.AspNetCore.Mvc;
using Models;



namespace Interfaces
{
    public interface IComentarioRepository
    {
        //Comentario Get(int Id);
        public Task<Comentario> Create(int idTopico,Comentario comentario);
        public Task<IEnumerable<Comentario>> Get(int idTopico);
        public Task<IEnumerable<Comentario>> Get(int idTopico,int id);
        public Task<Comentario> Update(int id,int idTopico,Comentario comentario);
        public Task<bool> Delete(int idTopico, int id);
    }
}
