using FluentValidation;

namespace AareonTechnicalTest.Application.Commands.Persons.Add
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonValidator()
        {
            RuleFor(request => request.Forename).NotEmpty();
            RuleFor(request => request.Surname).NotEmpty();
        }
    }
}