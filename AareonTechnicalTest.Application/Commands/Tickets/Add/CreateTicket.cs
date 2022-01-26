using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Tickets.Add
{
    public class CreateTicket : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
    {
        private readonly IDbContext _databaseContext;

        public CreateTicket(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var person = await _databaseContext.Persons.FirstOrDefaultAsync(person => person.Id == request.PersonId, cancellationToken).ConfigureAwait(false);

            if (person == null)
            {
                return new CreateTicketResponse
                {
                    Id = 0
                };
            }

            var ticket = new Ticket(request.Content, person);
            _databaseContext.Tickets.Add(ticket);
            await _databaseContext.SaveChangesAsync();
            return new CreateTicketResponse
            {
                Id = ticket.Id
            };
        }
    }
}