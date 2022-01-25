using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Commands.Tickets.Update
{
    public class UpdateTicket : IRequestHandler<UpdateTicketRequest, Unit>
    {
        private readonly IDbContext _databaseContext;

        public UpdateTicket(IDbContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }

        public async Task<Unit> Handle(UpdateTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _databaseContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == request.Id, cancellationToken).ConfigureAwait(false);

            if (ticket.CanUpdateTicket(request.Content))
            {
                ticket.UpdateContent(request.Content);
            }

            await _databaseContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}