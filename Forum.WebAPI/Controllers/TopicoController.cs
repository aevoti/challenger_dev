using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TopicoController : ControllerBase
{
    [HttpGet(Name = "GetTopico")]
    public IActionResult Get()
    {
        return Ok("Topicos: Aulas, Esportes, Filmes");
    }
}
