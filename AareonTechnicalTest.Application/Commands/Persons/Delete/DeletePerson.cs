using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Persons.Delete
{
    public class DeletePerson : IRequestHandler<DeletePersonRequest, Unit>
    {
        private readonly IDbContext _databaseContext;

        public DeletePerson(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _databaseContext.Persons.FirstOrDefaultAsync(person => person.Id == request.Id, cancellationToken);
            _databaseContext.Persons.Remove(person);
            await _databaseContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}