using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;

namespace webapi.Controllers
{
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ForumDbContext _context;

        public TestController(ForumDbContext context)
        {
            _context = context;
        }

        [HttpGet("connection")]
        public IActionResult TestConnection()
        {
            try
            {
                // Tente executar uma consulta simples no banco de dados
                var firstTopic = _context.Topics.FirstOrDefault();

                if (firstTopic != null)
                {
                    return Ok("A conexão com o banco de dados foi bem-sucedida!");
                }
                else
                {
                    return BadRequest("Não foi possível recuperar dados do banco de dados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }
    }
}
