using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AareonTechnicalTest.Application.Queries.Tickets.Get
{
    public class GetTicketValidator : AbstractValidator<GetTicketRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public GetTicketValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0).DependentRules(() =>
            {
                RuleFor(request => request.Id).Custom(CheckRecordExists);
            });
        }

        private void CheckRecordExists(int personId, ValidationContext<GetTicketRequest> customContext)
        {
            var person = _databaseContext.Tickets.Find(personId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Record Id : {personId}");
            }
        }
    }
}