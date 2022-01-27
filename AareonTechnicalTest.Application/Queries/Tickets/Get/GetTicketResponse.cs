using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Application.Queries.Tickets.Get
{
    public class GetTicketResponse
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Updated By
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Updated At
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}