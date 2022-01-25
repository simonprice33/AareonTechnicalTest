using MediatR;

namespace AareonTechnicalTest.Application.Commands.Persons.Add
{
    public class CreatePersonRequest : IRequest<CreatePersonResponse>
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }
}