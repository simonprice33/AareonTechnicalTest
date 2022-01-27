using System.Net.Mime;
using MediatR;

namespace AareonTechnicalTest.Application.Commands.Tickets.Update
{
    public class UpdateTicketRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Ticket Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Person Id Updating record
        /// </summary>
        public int PersonId { get; set; }
    }
}