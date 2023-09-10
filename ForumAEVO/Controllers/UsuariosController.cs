using ForumAEVO.Models;
using ForumAEVO.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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

        /// GET: api/usuarios/{email}
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
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

        // POST: api/usuarios
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna um exemplo personalizado", typeof(UsuarioDTO))]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
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

                //Configurando o usuário para aparecer alterado como resposta na API
                var usuarioDto = new UsuarioDTO
                {

                    Id = existingUser.Id,
                    Nome = existingUser.Nome,
                    Email = existingUser.Email,
                    Foto = existingUser.Foto
                };
                

                return Ok(usuarioDto);
            }

            return BadRequest(ModelState);
        }



        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(Guid id)
        {
            // buscando os topicos que o usuário fez
            var topicsToDelete = await _context.Topicos
                .Where(t => t.UserId == id)
                .ToListAsync();

            
            _context.Topicos.RemoveRange(topicsToDelete);

            // deletando o usuario sem problemas
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