using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDB.WebAPI.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IMDB.WebAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration; 
        public TokenService(IConfiguration configuration){
            this._configuration = configuration;
        }

        public string GerarToken(UsuarioDTO usuario){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("PrivateKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Regra.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}