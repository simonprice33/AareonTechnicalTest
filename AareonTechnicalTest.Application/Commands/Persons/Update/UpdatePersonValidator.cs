using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AareonTechnicalTest.Application.Queries;
using FluentValidation;

namespace AareonTechnicalTest.Application.Commands.Persons.Update
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonRequest>
    {
        private readonly IReadOnlyDbContext _databaseContext;

        public UpdatePersonValidator(IReadOnlyDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            RuleFor(request => request.Id).GreaterThan(0)
                .DependentRules(() =>
                {
                    RuleFor(request => request.Id).Custom(CheckRecordExists);
                });
        }

        private void CheckRecordExists(int personId, ValidationContext<UpdatePersonRequest> customContext)
        {
            var person = _databaseContext.Persons.Find(personId);

            if (person == null)
            {
                customContext.AddFailure($"Invalid Record Id : {personId}");
            }
        }
    }
}