using Microsoft.AspNetCore.Mvc;
using Models;
using Interfaces;
using Repositories;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicoController : Controller
    {
        private readonly ITopicoRepository _repository;

        public TopicoController(ITopicoRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetById(int Id)
        //{
            
        //    return  Ok(await _repository.GetById(Id));
        //}
        [HttpGet("{Ordenacao}")]
        public async Task<ActionResult> Get(int Ordenacao)
        {
            return Ok(await _repository.Get(Ordenacao));
        }

        [HttpGet("{textoPesquisa}/{Ordenacao}")]
        public async Task<ActionResult> Pesquisa(string textoPesquisa, int Ordenacao)
        {
            return Ok(await _repository.Pesquisa(textoPesquisa,Ordenacao));
        }

        [HttpPost]
        public async Task<ActionResult> Create(Topico topico)
        {
            if (topico.Dsc == null) return BadRequest(topico);
            return Ok(await _repository.Create(topico));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await _repository.Delete(Id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,Topico topico)
        {
            return Ok(await _repository.Update(id,topico));
        }
    }
}
