using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Delete;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Person
{
    public class Delete : EndpointBaseAsync
        .WithRequest<DeletePersonRequest>
        .WithActionResult<Unit>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Delete Person endpoint
        /// </summary>
        /// <param name="id">The request object for delete a person.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete(UrlConstants.PersonUrl + "/{Id}")]
        [SwaggerOperation(
            Summary = "Delete a Person",
            Description = "Delete a Person",
            OperationId = "Person.Delete",
            Tags = new[] { "Person" })
        ]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeletePersonRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}