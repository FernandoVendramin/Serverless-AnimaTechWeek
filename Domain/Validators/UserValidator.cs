using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Mail)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email invalido");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome invalido");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Sobrenome invalido");
        }
    }
}
