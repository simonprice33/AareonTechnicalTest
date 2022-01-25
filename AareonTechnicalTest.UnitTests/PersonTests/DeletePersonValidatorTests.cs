using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Delete;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries.Persons;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class DeletePersonValidatorTests
    {
        private readonly DeletePersonValidator _sut;
        private ApplicationContext _databaseContext;
        private Person _person;

        public DeletePersonValidatorTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task Validate_RequestIsValid_ReturnsTrue()
        {
            var request = new DeletePersonRequest()
            {
                Id = 1
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldNotHaveValidationErrorFor(request => request.Id);
        }

        [Fact]
        public async Task Validate_RequestIsValid_UserNotExist_ReturnsFalse()
        {
            var request = new DeletePersonRequest()
            {
                Id = 2
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(request => request.Id);
            result.Errors.FirstOrDefault().ErrorMessage.Should().Be("Invalid Record Id : 2");
        }

        [Fact]
        public async Task Validate_RequestIsValid_InvalidId_ReturnsFalse()
        {
            var request = new DeletePersonRequest()
            {
                Id = 0
            };

            var result = await _sut.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(request => request.Id);
            result.Errors.FirstOrDefault().ErrorMessage.Should().Be("'Id' must be greater than '0'.");
        }

        public DeletePersonValidator CreateSut()
        {
            return new DeletePersonValidator(_databaseContext);
        }
    }
}