using System.Threading.Tasks;
using IMDB.WebAPI.Models.Entidades;
using IMDB.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.WebAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class AdminController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public AdminController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("Usuarios")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var resultDTO = await _usuarioService.GetUsuariosPorRegra(Regras.User);
                return Ok(resultDTO);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }


    }
}