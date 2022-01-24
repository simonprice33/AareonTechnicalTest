using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using MediatR;

namespace AareonTechnicalTest.Application.Commands.Persons
{
    public class CreatePerson : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly IDbContext _databaseContext;

        public CreatePerson(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Forename, request.Surname, request.IsAdmin);
            _databaseContext.Persons.Add(person);
            await _databaseContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return new CreatePersonResponse
            {
                Id = person.Id
            };
        }
    }
}