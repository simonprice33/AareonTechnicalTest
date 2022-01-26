using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Delete;
using AareonTechnicalTest.Application.Commands.Tickets.Update;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Ticket
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateTicketRequest>
        .WithActionResult<Unit>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Delete Ticket endpoint
        /// </summary>
        /// <param name="request">The request object for Deleting a ticket.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut(UrlConstants.TicketUrl + "/{Id}")]
        [SwaggerOperation(
            Summary = "Update a Ticket",
            Description = "Update a Ticket",
            OperationId = "Ticket.Update",
            Tags = new[] { "Ticket" })
        ]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<Unit>> HandleAsync(UpdateTicketRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}