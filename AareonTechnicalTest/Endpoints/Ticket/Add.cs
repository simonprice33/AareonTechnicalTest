using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using AareonTechnicalTest.Application.Commands.Tickets.Add;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Ticket
{
    public class Add : EndpointBaseAsync
        .WithRequest<CreateTicketRequest>
        .WithActionResult<CreateTicketResponse>
    {
        private readonly IMediator _mediator;

        public Add(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Add Ticket endpoint
        /// </summary>
        /// <param name="request">The request object for adding a ticket.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost(UrlConstants.TicketUrl)]
        [SwaggerOperation(
            Summary = "Add a Ticket",
            Description = "Add a Ticket",
            OperationId = "Ticket.Add",
            Tags = new[] { "Ticket" })
        ]
        [ProducesResponseType(typeof(CreateTicketResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<CreateTicketResponse>> HandleAsync([FromBody] CreateTicketRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            return result.Id > 0
                ? Created(UrlConstants.TicketUrl + $"/{result.Id}", result.Id)
                : BadRequest("Unable to add ticket. Please check your inputs and try again");
        }
    }
}