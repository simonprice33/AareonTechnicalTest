using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AareonTechnicalTest.Application.Commands.Persons.Update
{
    public class UpdatePersonRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or Sets Person Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Forename
        /// </summary>
        public string Forename { get; set; }

        /// <summary>
        /// Gets or Sets Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or Sets Is Admin
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}