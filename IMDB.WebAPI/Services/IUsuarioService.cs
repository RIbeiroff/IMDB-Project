using System.Collections.Generic;
using System.Threading.Tasks;
using IMDB.WebAPI.Models.DTOs;
using IMDB.WebAPI.Models.Entidades;

namespace IMDB.WebAPI.Services
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Get todos os usuários
        /// </summary>
        /// <returns>Enumerable usuarios</returns>
        Task<IEnumerable<Usuario>> GetUsuarios();

        /// <summary>
        /// Get todos os usuários
        /// </summary>
        /// <returns>Enumerable usuarios</returns>
        Task<IEnumerable<UsuarioDTO>> GetUsuariosPorRegra(string Regra);

        /// <summary>
        /// Insert usuário
        /// </summary>
        /// <param name="usuario">Model usuario</param>
        /// <returns>Usuario</returns>
        Task<UsuarioDTO> InserirUsuario(UsuarioDTO usuario);

        /// <summary>
        /// Valida se já existe um registro com mesmo username no banco
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>True se for um username válido</returns>
        Task<bool> ValidarUsername(string username);        

        /// <summary>
        /// Deletear usuário por Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>True se o usuário foi deletado</returns>
        Task<bool> DeletarUsuarioPorId(int id);

        /// <summary>
        /// Get usuário por id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Model usuário</returns>
        Task<Usuario> GetUsuarioPorId(int id);   
    
        /// <summary>
        /// Atualizar usuário por id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="usuario">UsuarioDTO</param>
        /// <returns>UsarioDTO</returns>
        Task<UsuarioDTO> AtualizarUsuarioPorId(int id, UsuarioDTO usuarioDTO);

        /// <summary>
        /// Get usuário por login e senha
        /// </summary>
        /// <param name="userLogin">Model UsuarioDTO</param>
        /// <returns>UsuarioDTO</returns>
        Task<UsuarioDTO> GetUsuarioPorLogin(Login login);
    }
}