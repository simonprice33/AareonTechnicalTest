using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Queries
{
    public interface IReadOnlyDbContext
    {
        /// <summary>
        /// Gets or Sets Persons
        /// </summary>
        public DbSet<Person> Persons { get; }

        /// <summary>
        /// Gets or Sets Tickets
        /// </summary>
        public DbSet<Ticket> Tickets { get; }
    }
}