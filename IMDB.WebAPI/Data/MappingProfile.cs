using AutoMapper;
using IMDB.WebAPI.Models.DTOs;
using IMDB.WebAPI.Models.Entidades;

namespace IMDB.WebAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}