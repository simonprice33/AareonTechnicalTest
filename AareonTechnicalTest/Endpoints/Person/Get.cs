using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Queries.Persons;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Person
{
    public class Get : EndpointBaseAsync
        .WithRequest<GetPersonRequest>
        .WithActionResult<GetPersonResponse>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Update Person endpoint
        /// </summary>
        /// <param name="request">The request object for updating a person.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet(UrlConstants.PersonUrl + "/{Id}")]
        [SwaggerOperation(
            Summary = "Get a Person",
            Description = "Get a Person",
            OperationId = "Person.Get",
            Tags = new[] { "Person" })
        ]
        [ProducesResponseType(typeof(GetPersonResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<GetPersonResponse>> HandleAsync([FromRoute] GetPersonRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);
        }
    }
}