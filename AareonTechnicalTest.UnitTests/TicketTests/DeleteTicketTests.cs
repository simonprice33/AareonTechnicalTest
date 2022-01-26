using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Delete;
using AareonTechnicalTest.Application.Commands.Tickets.Delete;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class DeleteTicketTests
    {
        private ApplicationContext _databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private Ticket _ticket;
        private readonly DeleteTicket _sut;

        public DeleteTicketTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person)
                .InterimBuild()
                .AddTicket("ticket content", _person, out _ticket)
                .Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task DeleteTicket_ReturnsUnit()
        {
            var request = new DeleteTicketRequest()
            {
                Id = 1
            };

            _databaseContext.Tickets.ToList().Should().HaveCount(1);
            _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _databaseContext.Tickets.ToList().Should().HaveCount(0);
        }

        private DeleteTicket CreateSut()
        {
            return new DeleteTicket(_databaseContext);
        }
    }
}