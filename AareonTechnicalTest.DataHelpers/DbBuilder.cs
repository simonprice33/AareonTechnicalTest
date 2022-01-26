using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Add;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.DataHelpers
{
    public class DbBuilder : IDisposable
    {
        public DbBuilder()
        {
            DbContext = SqLiteContextOptionsBuilder.Create(Id);
        }

        public DbBuilder(bool buildTestData)
        {
            DbContext = SqLiteContextOptionsBuilder.Create(Id);
        }

        public ApplicationContext DbContext { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public async Task<ApplicationContext> BuildAsync()
        {
            await DbContext.SaveChangesAsync();
            return DbContext;
        }

        public ApplicationContext Build()
        {
            var timeout = TimeSpan.FromSeconds(3);
            var task = BuildAsync();
            task.Wait(timeout);
            return DbContext;
        }

        public DbBuilder InterimBuild()
        {
            Build();
            return this;
        }

        public void Dispose()
        {
            if (DbContext == null) return;
            DbContext.Database.CloseConnection();
            DbContext.Database.GetDbConnection().Dispose();
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }

        public DbBuilder AddPerson(string forename, string surname, bool isAdmin, out Person person)
        {
            person = new Person(forename, surname, true);
            DbContext.Persons.Add(person);
            return this;
        }

        public DbBuilder AddTicket(string content, Person person, out Ticket ticket)
        {
            ticket = new Ticket(content, person);
            DbContext.Tickets.Add(ticket);
            return this;
        }
    }
}