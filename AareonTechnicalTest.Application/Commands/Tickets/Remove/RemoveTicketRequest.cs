using MediatR;

namespace AareonTechnicalTest.Application.Commands.Tickets.Remove
{
    public class RemoveTicketRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or Sets Person Id
        /// </summary>
        public int PersonId { get; set; }
    }
}