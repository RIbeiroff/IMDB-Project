using FluentValidation;
using IMDB.WebAPI.Models.DTOs;

namespace IMDB.WebAPI.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator(){
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("Nome inválido");
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username inválido");             
            RuleFor(u => u.Senha)
                .Password();
        
        }
    }
}