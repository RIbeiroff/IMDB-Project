using FluentValidation;

namespace IMDB.WebAPI.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                          .NotEmpty()
                          .NotNull()
                          .MinimumLength(8)
                          .MaximumLength(16)
                          .Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$")
                          .WithMessage("Senha inválida: A senha deve possuir entre 8 a 16 dígitos e ao menos uma letra");

            return options;
        }
    }
}