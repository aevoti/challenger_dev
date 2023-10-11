using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
