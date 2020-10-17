using System.Threading.Tasks;
using IMDB.WebAPI.Data;
using IMDB.WebAPI.Models.Entidades;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMDB.WebAPI.Models.DTOs;
using AutoMapper;
using System;

namespace IMDB.WebAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _repositorio;
        private readonly IEncriptacaoService _encriptacaoService;
        private readonly IMapper _mapper;

        public UsuarioService(DataContext repositorio,
                              IEncriptacaoService encriptacaoService,
                              IMapper mapper)
        {
            this._repositorio = repositorio;
            this._encriptacaoService = encriptacaoService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _repositorio.BD_Usuarios.ToListAsync();
        }

        public async Task<UsuarioDTO> InserirUsuario(UsuarioDTO usuarioDTO)
        {
            var result = await ValidarUsername(usuarioDTO.Username);
            if (result)
            {
                var userData = _mapper.Map<Usuario>(usuarioDTO);

                PrepararUsuarioParaPersistencia(userData);
                await _repositorio.BD_Usuarios.AddAsync(userData);
                await _repositorio.SaveChangesAsync();

                userData.Senha = string.Empty;
                return _mapper.Map<UsuarioDTO>(userData); ;
            }
            return null;
        }

        private async Task<Usuario> GetUsuarioPorUsername(string username)
        {
            var query = from u in _repositorio.BD_Usuarios
                        where u.Username.Equals(username) && !u.Deletado
                        select u;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ValidarUsername(string username)
        {
            var query = from u in _repositorio.BD_Usuarios
                        where u.Username.Equals(username) && !u.Deletado
                        select u;

            var result = await query.FirstOrDefaultAsync();

            return result == null ? true : false;
        }

        private void PrepararUsuarioParaPersistencia(Usuario usuario)
        {
            usuario.Username = usuario.Username.Trim().ToLower();
            usuario.ChaveSenha = _encriptacaoService.CriarChave(5);
            usuario.Senha = _encriptacaoService.CriarHashSenha(usuario.Senha, usuario.ChaveSenha);
            usuario.Deletado = false;
        }
        public async Task<Usuario> GetUsuarioPorId(int id)
        {
            return await _repositorio.BD_Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> DeletarUsuarioPorId(int id)
        {
            var usuario = await GetUsuarioPorId(id);

            if (usuario != null)
            {
                usuario.Deletado = true;
                await UpdateUsuarioRepository(usuario);

                return true;
            }

            return false;
        }

        private async Task UpdateUsuarioRepository(Usuario usuario)
        {
            var userData = await GetUsuarioPorId(usuario.Id);
            if (userData != null)
            {
                userData.Nome = usuario.Nome;
                userData.Regra = usuario.Regra;
                userData.ChaveSenha = usuario.ChaveSenha;
                userData.Senha = usuario.Senha;
                userData.Deletado = usuario.Deletado;
                userData.Username = usuario.Username.ToLower();
                userData.Deletado = usuario.Deletado;
                await _repositorio.SaveChangesAsync();
            }
        }

        public async Task<UsuarioDTO> AtualizarUsuarioPorId(int id, UsuarioDTO usuarioDTO)
        {
            var userData = await GetUsuarioPorId(id);

            if (userData == null)
                return null;

            if (!userData.Username.Equals(usuarioDTO.Username))
            {
                var result = await ValidarUsername(usuarioDTO.Username);
                if (!result)
                {
                    return null;
                }
            }

            var resultUser = _mapper.Map<Usuario>(usuarioDTO);

            PrepararUsuarioParaPersistencia(resultUser);
            await UpdateUsuarioRepository(resultUser);

            resultUser.Senha = string.Empty;
            return _mapper.Map<UsuarioDTO>(resultUser); ;
        }

        public async Task<UsuarioDTO> GetUsuarioPorLogin(Login login)
        {
            var userData = await GetUsuarioPorUsername(login.Username);
            if (userData == null)
                return null;
            var hashSenha =  _encriptacaoService.CriarHashSenha(login.Senha, userData.ChaveSenha);
            if (userData.Senha.Equals(hashSenha)){
                var usuarioDTO = _mapper.Map<UsuarioDTO>(userData);
                usuarioDTO.Senha = string.Empty;
                return usuarioDTO;
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosPorRegra(string Regra){
            var query = from u in _repositorio.BD_Usuarios
                        where u.Regra.Equals(Regra) && !u.Deletado
                        select u;
            var list = await query.ToListAsync();
            var result = list.Select(c => { return _mapper.Map<UsuarioDTO>(c); }).ToList(); 
            result.ForEach(r => r.Senha = string.Empty);
            return result;
        }

    }
}