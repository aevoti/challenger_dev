using ForumMinimalAPI.Models;
using ForumMinimalAPI.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace ForumMinimalAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("obterUsuario/{usuarioId}")]
        public async Task<ActionResult<Usuario>> ObterUsuarioPorId(int usuarioId)
        {
            var usuario = await _authService.ObterUsuarioPorId(usuarioId);

            if (usuario != null)
            {
                return Ok(usuario);
            }

            return NotFound();
        }

        [HttpPost("registro")]
        public async Task<ActionResult<string>> RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var token = await _authService.RegistrarUsuario(usuario);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] Usuario usuario)
        {
            try
            {
                var token = await _authService.Login(usuario);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}