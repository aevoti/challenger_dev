using Microsoft.AspNetCore.Mvc;
using Models;
namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForumController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Topico> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Topico
            {
                Id = index
            })
            .ToArray();
        }

      
    }
}
