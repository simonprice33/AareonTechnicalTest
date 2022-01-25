using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Persons.Update
{
    public class UpdatePerson : IRequestHandler<UpdatePersonRequest, Unit>
    {
        private readonly IDbContext _databaseContext;

        public UpdatePerson(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Unit> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _databaseContext.Persons.FirstOrDefaultAsync(person => person.Id == request.Id, cancellationToken);

            if (person.CanUpdateForename(request.Forename))
            {
                person.UpdateForename(request.Forename);
            }

            if (person.CanUpdateSurname(request.Surname))
            {
                person.UpdateSurname(request.Surname);
            }

            if (person.CanUpdateAdminStatus(request.IsAdmin))
            {
                person.UpdateAdminStatus(request.IsAdmin);
            }

            await _databaseContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}