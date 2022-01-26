using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Delete;

using AareonTechnicalTest.Application.Commands.Tickets.Delete;

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Ticket
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteTicketRequest>.WithActionResult<Unit>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Delete Ticket endpoint
        /// </summary>
        /// <param name="request">The request object for Deleteing a ticket.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete(UrlConstants.TicketUrl + "/{Id}/admin/{PersonId}")]
        [SwaggerOperation(
            Summary = "Delete a Ticket",
            Description = "Delete a Ticket",
            OperationId = "Ticket.Delete",
            Tags = new[] { "Ticket" })
        ]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<Unit>> HandleAsync([FromRoute] DeleteTicketRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}