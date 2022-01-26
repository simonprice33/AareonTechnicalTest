using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using AareonTechnicalTest.Application.Queries;
using FluentValidation;

namespace AareonTechnicalTest.Application.Commands.Tickets.Update
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public UpdateTicketValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0)
                .DependentRules(() =>
                {
                    RuleFor(request => request.Id).Custom(CheckRecordExists);
                });
        }

        private void CheckRecordExists(int ticketId, ValidationContext<UpdateTicketRequest> customContext)
        {
            var person = _databaseContext.Tickets.Find(ticketId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Record Id : {ticketId}");
            }
        }
    }
}