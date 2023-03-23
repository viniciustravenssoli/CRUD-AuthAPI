using FluentValidation;
using Manager.Domain.Entities;

//using fluent validation

namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .MinimumLength(3)
                .WithMessage("O nome deve ter 3 ou mais caracteres")

                .MaximumLength(80)
                .WithMessage("O Nome deve ter no maximo 80 caracteres");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A senha nao pode ser nula")

                .NotEmpty()
                .WithMessage("A senha nao pode ser vazia")

                .MinimumLength(8)
                .WithMessage("A senha deve ter 8 ou mais caracteres")

                .MaximumLength(60)
                .WithMessage("A senha deve ter no maximo 60 caracteres");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O email nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O email não pode ser vazio")

                .MinimumLength(8)
                .WithMessage("O email deve ter no minimo 10 caracteres")

                .MaximumLength(180)
                .WithMessage("O email deve ter no maximo 180 caracteres")

                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido.");
        }
    }
}