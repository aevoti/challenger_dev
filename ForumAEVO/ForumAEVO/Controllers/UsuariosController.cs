using ForumAEVO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly Context _context;

    public UsuariosController(Context context)
    {
        _context = context;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
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