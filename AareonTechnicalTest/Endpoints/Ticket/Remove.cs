using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Remove;

using AareonTechnicalTest.Application.Commands.Tickets.Remove;

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Ticket
{
    public class Remove : EndpointBaseAsync.WithRequest<RemoveTicketRequest>.WithActionResult<Unit>
    {
        private readonly IMediator _mediator;

        public Remove(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Remove Ticket endpoint
        /// </summary>
        /// <param name="request">The request object for Remove a ticket.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete(UrlConstants.TicketUrl + "/{Id}/remove")]
        [SwaggerOperation(
            Summary = "Remove a Ticket",
            Description = "Remove a Ticket",
            OperationId = "Ticket.Remove",
            Tags = new[] { "Ticket" })
        ]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<Unit>> HandleAsync([FromRoute] RemoveTicketRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}