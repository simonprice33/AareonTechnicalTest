using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Delete;
using AareonTechnicalTest.Application.Commands.Tickets.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class UpdateTicketTests
    {
        private ApplicationContext _databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private Ticket _ticket;
        private readonly UpdateTicket _sut;

        public UpdateTicketTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person)
                .InterimBuild()
                .AddTicket("ticket content", _person, out _ticket)
                .Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task UpdateTicket_ReturnsNoContent()
        {
            var request = new UpdateTicketRequest
            {
                Id = 1,
                PersonId = 1,
                Content = "Updated Content"
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbValue = _databaseContext.Tickets.FirstOrDefault(ticket => ticket.Id == 1);
            dbValue.Content.Should().Be("Updated Content");
        }

        private UpdateTicket CreateSut()
        {
            return new UpdateTicket(_databaseContext);
        }
    }
}