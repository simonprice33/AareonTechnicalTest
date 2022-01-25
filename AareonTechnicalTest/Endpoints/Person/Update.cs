using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Person
{
    public class Update : EndpointBaseAsync
    .WithRequest<UpdatePersonRequest>
        .WithActionResult<UpdatePersonResponse>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Update Person endpoint
        /// </summary>
        /// <param name="request">The request object for updating a person.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut(UrlConstants.PersonUrl + "/{Id}")]
        [SwaggerOperation(
            Summary = "Update a Person",
            Description = "Update a Person",
            OperationId = "Person.Update",
            Tags = new[] { "Person" })
        ]
        [ProducesResponseType(typeof(UpdatePersonResponse), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<UpdatePersonResponse>> HandleAsync(UpdatePersonRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken);
            return NoContent();
        }
    }
}