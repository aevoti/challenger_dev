using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumAEVO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly Context _context;

        public UsuariosController(Context context)
        {
            _context = context;
        }


        private async Task<IActionResult> Get()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var usuario = await _context.Usuarios
               .Where(u => u.Email == email)
               .ToListAsync(); 

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado pelo Email fornecido.");
            }
            
            var usuarioDto = usuario.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Foto = usuario.Foto,
            }).ToList();

            return Ok(usuarioDto);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Usuario updateUsuario)
        {
            var existingUser = await _context.Usuarios.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingUser.Nome = updateUsuario.Nome;
                existingUser.Email = updateUsuario.Email;
                existingUser.Foto = updateUsuario.Foto;

                try
                {
                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}