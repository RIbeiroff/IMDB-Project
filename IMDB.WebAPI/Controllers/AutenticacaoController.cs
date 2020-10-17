using System.Threading.Tasks;
using IMDB.WebAPI.Models.DTOs;
using IMDB.WebAPI.Models.Entidades;
using IMDB.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.WebAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(IUsuarioService usuarioService,
                                      ITokenService tokenService)
        {
            this._usuarioService = usuarioService;
            this._tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Post([FromBody] Login login)
        {
            try
            {
                var user = await _usuarioService.GetUsuarioPorLogin(login);

                if (user == null)
                    return this.StatusCode(StatusCodes.Status409Conflict, "Login ou senha inválidos");

                var token = _tokenService.GerarToken(user);

                var result =  new
                {
                    user = user,
                    token = token
                };

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }

    }
}