using Microsoft.AspNetCore.Mvc;

namespace Forum.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ComentarioController : ControllerBase
{
    [HttpGet(Name = "GetComentario")]
    public IActionResult Get()
    {
        return Ok("Comentario: Aulas, Esportes, Filmes");
    }
}
