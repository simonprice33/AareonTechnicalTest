using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Queries;
using FluentValidation;

namespace AareonTechnicalTest.Application.Commands.Persons.Delete
{
    public class DeletePersonValidator : AbstractValidator<DeletePersonRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public DeletePersonValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0).DependentRules(() =>
            {
                RuleFor(request => request.Id).Custom(CheckRecordExists);
            });
        }

        private void CheckRecordExists(int personId, ValidationContext<DeletePersonRequest> customContext)
        {
            var person = _databaseContext.Persons.Find(personId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Record Id : {personId}");
                return;
            }

            if (!person.IsAdmin)
            {
                customContext.AddFailure("This function can ony be completed by an Admin");
            }
        }
    }
}