using System.Linq;
using AareonTechnicalTest.Application.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Tickets.Remove
{
    public class RemoveTicketValidator : AbstractValidator<RemoveTicketRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public RemoveTicketValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0)
                .DependentRules(() =>
                {
                    RuleFor(request => request.Id).Custom(CheckRecordExists);
                });

            RuleFor(request => request.PersonId).GreaterThan(0)
                .DependentRules(() =>
                {
                    RuleFor(request => request.PersonId).Custom(CheckPersonExists);
                });
        }

        private void CheckPersonExists(int personId, ValidationContext<RemoveTicketRequest> customContext)
        {
            var person = _databaseContext.Persons.FirstOrDefault(person => person.Id == personId);
            if (person == null)
            {
                customContext.AddFailure($"Invalid Id : {personId}");
                return;
            }
        }

        private void CheckRecordExists(int ticketId, ValidationContext<RemoveTicketRequest> customContext)
        {
            var ticket = _databaseContext.Tickets
                .Include(person => person.Person)
                .FirstOrDefault(ticket => ticket.Id == ticketId);

            if (ticket == null || ticket.IsRemoved)
            {
                customContext.AddFailure($"Invalid Record Id : {ticketId}");
                return;
            }
        }
    }
}