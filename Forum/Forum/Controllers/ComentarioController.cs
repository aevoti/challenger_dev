using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : Controller
    {


        private readonly IComentarioRepository _repository;

        public ComentarioController(IComentarioRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("{idTopico}")]
        public async Task<ActionResult> Get(int idTopico)
        {
            return Ok(await _repository.Get(idTopico));
        }

        [HttpGet("{idTopico}/{id}")]
        public async Task<ActionResult> GetById(int idTopico,int id)
        {
            return Ok(await _repository.Get(idTopico,id));
        }

        [HttpPost("{idTopico}")]
        public async Task<ActionResult> Create(int idTopico,Comentario comentario)
        {
            return Ok(await _repository.Create(idTopico,comentario));
        }
            
        [HttpDelete("{idTopico}/{id}")]
        public async Task<ActionResult> Delete(int idTopico, int id)
        {
            return Ok(await _repository.Delete(idTopico,id));
        }

        [HttpPut("{idTopico}/{id}")]
        public async Task<ActionResult> Update(int idTopico, int id,Comentario cComentario)
        {
            return Ok(await _repository.Update(id, idTopico, cComentario));
        }
    }
}
