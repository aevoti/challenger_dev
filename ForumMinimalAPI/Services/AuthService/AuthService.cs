using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ForumMinimalAPI.Data;
using ForumMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ForumMinimalAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public AuthService(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<Usuario> ObterUsuarioPorId(int usuarioId)
        {
            try
            {
                var usuario = await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);

                if (usuario != null)
                {
                    return usuario;
                }

                return null;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao buscar usuário por ID. Detalhes do erro: ");
            }
        }

        public async Task<string> RegistrarUsuario(Usuario usuario)
        {
            try
            {
                var usuarioExiste = await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

                if (usuarioExiste != null)
                {
                    throw new Exception("Já existe um usuário com este email.");
                }

                var token = GenerateJwtToken(usuario.Email ,usuario.Nome, usuario.Id);

                if (!string.IsNullOrEmpty(token))
                {
                    string senhaBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Senha));

                    usuario.Senha = senhaBase64;

                    _dataContext.Usuarios.Add(usuario);
                    await _dataContext.SaveChangesAsync();
                }

                return token;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao cadastrar o usuário. Detalhes do erro: ");
            }
        }

        public async Task<string> Login(Usuario usuario)
        {
            var usuarioExiste = await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (usuarioExiste != null)
            {
                string senhaBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Senha));

                if (usuarioExiste.Senha == senhaBase64)
                {
                    return GenerateJwtToken(usuarioExiste.Email ,usuarioExiste.Nome, usuarioExiste.Id);
                }
            }

            throw new Exception("Login falhou. Verifique suas credenciais.");
        }

        private string GenerateJwtToken(string email,string nome, int id)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("nome", nome),
            new Claim("id", id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("M@Qw8$y3p!LZ#s6rT&5tA%g2xJpF*dG7\r\n"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}