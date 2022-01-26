using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Tickets.Remove
{
    public class RemoveTicket : IRequestHandler<RemoveTicketRequest, Unit>
    {
        private readonly IDbContext _databaseContext;

        public RemoveTicket(IDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Unit> Handle(RemoveTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _databaseContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken).ConfigureAwait(false);
            if (ticket.CanRemove())
            {
                ticket.Remove();
            }

            await _databaseContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}