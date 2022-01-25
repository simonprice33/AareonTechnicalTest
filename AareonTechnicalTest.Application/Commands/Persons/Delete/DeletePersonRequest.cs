using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AareonTechnicalTest.Application.Commands.Persons.Delete
{
    public class DeletePersonRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }
    }
}