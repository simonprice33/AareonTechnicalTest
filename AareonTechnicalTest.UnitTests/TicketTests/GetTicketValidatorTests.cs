using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries.Tickets.Get;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class GetTicketValidatorTests
    {
        private ApplicationContext _databaseContext;
        private Person _person;
        private Ticket _ticket;
        private readonly GetTicketValidator _sut;

        public GetTicketValidatorTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person)
                .InterimBuild()
                .AddTicket("ticket content", _person, out _ticket)
                .Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task Validate_ReturnsTrue()
        {
            var request = new GetTicketRequest
            {
                Id = 1
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_InvalidId_ReturnsFalse()
        {
            var request = new GetTicketRequest()
            {
                Id = 0
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.Id);
        }

        [Fact]
        public async Task Validate_RecordNotExist_ReturnsFalse()
        {
            var request = new GetTicketRequest
            {
                Id = 9999
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.Id);
            result.Errors.FirstOrDefault().ErrorMessage.Should().Be("Invalid Record Id : 9999");
        }

        private GetTicketValidator CreateSut()
        {
            return new GetTicketValidator(_databaseContext);
        }
    }
}