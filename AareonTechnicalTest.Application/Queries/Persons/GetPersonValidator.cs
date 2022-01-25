using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AareonTechnicalTest.Application.Queries.Persons
{
    public class GetPersonValidator : AbstractValidator<GetPersonRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public GetPersonValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0).DependentRules(() =>
            {
                RuleFor(request => request.Id).Custom(CheckRecordExists);
            });
        }

        private void CheckRecordExists(int personId, ValidationContext<GetPersonRequest> customContext)
        {
            var person = _databaseContext.Persons.Find(personId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Record Id : {personId}");
            }
        }
    }
}