using AareonTechnicalTest.Application.Queries;
using FluentValidation;

namespace AareonTechnicalTest.Application.Commands.Tickets.Add
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public CreateTicketValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Content).NotEmpty();
            RuleFor(request => request.PersonId).GreaterThan(0)
                .DependentRules(() =>
                {
                    RuleFor(request => request.PersonId).Custom(CheckRecordExists);
                });
        }

        private void CheckRecordExists(int personId, ValidationContext<CreateTicketRequest> customContext)
        {
            var person = _databaseContext.Persons.Find(personId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Person Record Id : {personId}");
            }
        }
    }
}