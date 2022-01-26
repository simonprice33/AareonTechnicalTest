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
        }

        private void CheckRecordExists(int ticketId, ValidationContext<RemoveTicketRequest> customContext)
        {
            var ticket = _databaseContext.Tickets
                .Include(person => person.Person)
                .FirstOrDefault(ticket => ticket.Id == ticketId);

            if (ticket == null)
            {
                customContext.AddFailure($"Invalid Record Id : {ticketId}");
                return;
            }
        }
    }
}