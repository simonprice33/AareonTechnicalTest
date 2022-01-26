using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.MappingProfiles;
using AareonTechnicalTest.Application.Queries.Tickets.Get;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class GetTicketTests
    {
        private ApplicationContext _databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private Ticket _ticket;
        private readonly GetTicket _sut;

        public GetTicketTests()
        {
            _mapper = new Mapper(new MapperConfiguration(expression =>
                expression.AddProfiles(new List<Profile>()
                {
                    new GetTicketProfile()
                })
            ));

            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person)
                .InterimBuild()
                .AddTicket("ticket content", _person, out _ticket)
                .Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task GetTicket_ReturnsGetTicketResponse()
        {
            var request = new GetTicketRequest
            {
                Id = 1
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            result.Should().BeOfType<GetTicketResponse>();
            result.Content.Should().Be("ticket content");
            result.Id.Should().Be(1);
            result.CreatedBy.Should().Be("Simon Price");
        }

        [Fact]
        public async Task GetTicket_ReturnsEmptyGetTicketResponse()
        {
            var request = new GetTicketRequest
            {
                Id = 999
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            result.Should().BeOfType<GetTicketResponse>();
            result.Id.Should().Be(0);
            result.Content.Should().Be(null);
            result.CreatedBy.Should().Be(null);
        }

        private GetTicket CreateSut()
        {
            return new GetTicket(_databaseContext, _mapper);
        }
    }
}