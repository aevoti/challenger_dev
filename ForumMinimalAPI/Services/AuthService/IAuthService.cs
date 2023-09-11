using ForumMinimalAPI.Models;

namespace ForumMinimalAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> RegistrarUsuario(Usuario usuario);
        Task<string> Login(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(int usuarioId);
    }
}