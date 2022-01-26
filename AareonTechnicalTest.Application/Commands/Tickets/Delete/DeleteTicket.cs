using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Tickets.Delete
{
    public class DeleteTicket : IRequestHandler<DeleteTicketRequest, Unit>
    {
        private readonly IDbContext _databaseContext;

        public DeleteTicket(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Unit> Handle(DeleteTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _databaseContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken).ConfigureAwait(false);
            _databaseContext.Tickets.Remove(ticket);
            await _databaseContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}