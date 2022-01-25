using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Application.Queries.Persons
{
    public class GetPersonResponse
    {
        /// <summary>
        /// Gets or Sets Id
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