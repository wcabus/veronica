using System;
using FluentValidation;

namespace Veronica.Backend.Application.Users
{
    public class RegisterUser : ICommand
    {
        public RegisterUser()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string Email { get; set; }
        public string Name { get; set; }

        public DateTimeOffset RegistrationDate { get; internal set; }
    }

    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
