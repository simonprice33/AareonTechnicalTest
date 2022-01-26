using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Application.Queries.Tickets.Get
{
    public class GetTicketResponse
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }
    }
}