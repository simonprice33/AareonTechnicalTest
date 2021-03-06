using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Remove;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class RemoveTicketValidatorTests
    {
        private ApplicationContext _databaseContext;
        private Person _person;
        private Ticket _ticket;
        private readonly RemoveTicketValidator _sut;

        public RemoveTicketValidatorTests()
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
            var request = new RemoveTicketRequest
            {
                Id = 1,
                PersonId = 1
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_InvalidId_ReturnsFalse()
        {
            var request = new RemoveTicketRequest
            {
                Id = 0
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.Id);
        }

        [Fact]
        public async Task Validate_PersonNotExists_ReturnsFalse()
        {
            var request = new RemoveTicketRequest
            {
                Id = 1,
                PersonId = 999
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.PersonId);
        }

        [Fact]
        public async Task Validate_RecordNotExist_ReturnsFalse()
        {
            var request = new RemoveTicketRequest
            {
                Id = 2
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.Id);
            result.Errors.FirstOrDefault().ErrorMessage.Should().Be("Invalid Record Id : 2");
        }

        private RemoveTicketValidator CreateSut()
        {
            return new RemoveTicketValidator(_databaseContext);
        }
    }
}