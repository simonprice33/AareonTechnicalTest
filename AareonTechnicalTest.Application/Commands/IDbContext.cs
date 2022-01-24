using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Application.Commands
{
    public interface IDbContext
    {
        /// <summary>
        /// Gets or Sets Persons
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Gets or Sets Tickets
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; }
    }
}