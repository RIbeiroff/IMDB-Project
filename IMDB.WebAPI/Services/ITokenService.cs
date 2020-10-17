using IMDB.WebAPI.Models.DTOs;

namespace IMDB.WebAPI.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Gerar token para usuário
        /// </summary>
        /// <param name="usuario">Model UsuarioDTO</param>
        /// <returns>Token</returns>
        string GerarToken(UsuarioDTO usuario);
    }
}