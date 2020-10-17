using FluentValidation;
using IMDB.WebAPI.Models.Entidades;

namespace IMDB.WebAPI.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator(){
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username inválido");
            RuleFor(u => u.Senha)
                .NotEmpty()
                .WithMessage("Senha inválida")
                .Password(); 
        }
    }

}