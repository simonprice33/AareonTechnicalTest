using System;
using System.Collections.Generic;
using Ardalis.ApiEndpoints;
using MediatR;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AareonTechnicalTest.Endpoints.Person
{
    public class Add : EndpointBaseAsync
        .WithRequest<CreatePersonRequest>
        .WithActionResult<CreatePersonResponse>
    {
        private readonly IMediator _mediator;

        public Add(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Handler for the Add Person endpoint
        /// </summary>
        /// <param name="request">The request object for adding a person.</param>
        /// <param name="cancellationToken">An instance of <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost(UrlConstants.PersonUrl)]
        [SwaggerOperation(
            Summary = "Add a Person",
            Description = "Add a Person",
            OperationId = "Person.Add",
            Tags = new[] { "Person" })
        ]
        [ProducesResponseType(typeof(CreatePersonResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public override async Task<ActionResult<CreatePersonResponse>> HandleAsync(CreatePersonRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request);
            return Created(UrlConstants.PersonUrl + $"/{result.Id}", result.Id);
        }
    }
}