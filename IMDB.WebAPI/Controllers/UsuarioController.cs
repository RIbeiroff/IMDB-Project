using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.WebAPI.Models.DTOs;
using IMDB.WebAPI.Models.Entidades;
using IMDB.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IMDB.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioDTO userRequest)
        {
            try
            {
                var result = await _usuarioService.InserirUsuario(userRequest);
                if (result != null)
                {
                    return Ok(result);
                }
                return this.StatusCode(StatusCodes.Status409Conflict, "Já existe um usuário com este username");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _usuarioService.DeletarUsuarioPorId(id);
                return result ? Ok(result) : this.StatusCode(StatusCodes.Status409Conflict, "Não existe user com este id");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UsuarioDTO usuario)
        {
            try
            {
                if (id != usuario.Id){
                    return BadRequest();
                }
                
                var result = await _usuarioService.AtualizarUsuarioPorId(id, usuario);
   
                if (result == null){
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
                }
   
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var resultDTO = await _usuarioService.GetUsuarios();
                return Ok(resultDTO);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }


    }
}