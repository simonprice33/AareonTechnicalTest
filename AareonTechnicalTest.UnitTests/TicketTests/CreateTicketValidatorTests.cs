using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Tickets.Add;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using FluentValidation.TestHelper;
using Xunit;

namespace AareonTechnicalTest.UnitTests.TicketTests
{
    public class CreateTicketValidatorTests
    {
        private readonly ApplicationContext _databaseContext;
        private Person _person;
        private readonly CreateTicketValidator _sut;

        public CreateTicketValidatorTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task Validate_ReturnsTrue()
        {
            var request = new CreateTicketRequest
            {
                PersonId = 1,
                Content = "Test content"
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_NoContent_ReturnsFalse()
        {
            var request = new CreateTicketRequest
            {
                PersonId = 1,
                Content = string.Empty
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.Content);
        }

        [Fact]
        public async Task Validate_NoPerson_ReturnsFalse()
        {
            var request = new CreateTicketRequest
            {
                PersonId = 1000,
                Content = string.Empty
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(ticket => ticket.PersonId);
        }

        private CreateTicketValidator CreateSut()
        {
            return new CreateTicketValidator(_databaseContext);
        }
    }
}