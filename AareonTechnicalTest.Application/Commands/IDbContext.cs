using AareonTechnicalTest.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        /// <summary>
        /// Save changes async with default options.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}