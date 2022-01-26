using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Queries.Tickets;
using AareonTechnicalTest.Application.Queries.Tickets.Get;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Ticket
{
    public class Get : EndpointBaseAsync
        .WithRequest<GetTicketRequest>
        .WithActionResult<GetTicketResponse>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Update Ticket endpoint
        /// </summary>
        /// <param name="request">The request object for updating a Ticket.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet(UrlConstants.TicketUrl + "/{Id}")]
        [SwaggerOperation(
            Summary = "Get a Ticket",
            Description = "Get a Ticket",
            OperationId = "Ticket.Get",
            Tags = new[] { "Ticket" })
        ]
        [ProducesResponseType(typeof(GetTicketResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<GetTicketResponse>> HandleAsync([FromRoute] GetTicketRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);
        }
    }
}