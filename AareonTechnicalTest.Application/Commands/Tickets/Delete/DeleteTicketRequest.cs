using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AareonTechnicalTest.Application.Commands.Tickets.Delete
{
    public class DeleteTicketRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }
    }
}