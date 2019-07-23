using System;
using FluentValidation;

namespace Veronica.Backend.Application.Dishes
{
    public class CreateDish : ICommand
    {
        public CreateDish()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal? Score { get; set; }
        public DateTimeOffset? LastInMenu { get; set; }

        public DateTimeOffset Added { get; internal set; }
    }

    public class CreateDishValidator : AbstractValidator<CreateDish>
    {
        public CreateDishValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 5);
        }
    }
}