﻿using MediatR;

namespace AareonTechnicalTest.Application.Commands.Tickets.Add
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets or Sets Person Id
        /// </summary>
        public int PersonId { get; private set; }
    }
}