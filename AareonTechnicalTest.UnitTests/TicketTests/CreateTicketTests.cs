using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using AareonTechnicalTest.Application.Commands.Tickets.Add;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class CreateTicketTests
    {
        private readonly ApplicationContext _databaseContext;
        private readonly CreateTicket _sut;
        private Person _person;

        public CreateTicketTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();
            _sut = CreateSut();
        }

        [Fact]
        public async Task CreateTicket_ReturnsCreateTicketResponse()
        {
            var request = new CreateTicketRequest
            {
                PersonId = 1,
                Content = "This is test ticket"
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            result.Id.Should().Be(1);
        }

        [Fact]
        public async Task CreateTicket_PersonNotExist_ReturndId_0()
        {
            var request = new CreateTicketRequest
            {
                PersonId = 999,
                Content = "This is test ticket"
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            result.Id.Should().Be(0);
        }

        private CreateTicket CreateSut()
        {
            return new CreateTicket(_databaseContext);
        }
    }
}