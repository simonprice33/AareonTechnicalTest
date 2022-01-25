using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using FluentValidation.TestHelper;
using Xunit;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class CreatePersonValidatorTests
    {
        private readonly CreatePersonValidator _sut;

        public CreatePersonValidatorTests()
        {
            _sut = CreateSut();
        }

        [Fact]
        public async Task Validate_RequestFornameInvalid_ReturnsFalse()
        {
            var request = new CreatePersonRequest
            {
                Forename = string.Empty,
                Surname = "Price",
                IsAdmin = true
            };

            var result = await _sut.TestValidateAsync(request).ConfigureAwait(false);
            result.ShouldHaveValidationErrorFor(request => request.Forename);
        }

        [Fact]
        public async Task Validate_RequestSurnameInvalid_ReturnsFalse()
        {
            var request = new CreatePersonRequest
            {
                Forename = "Simon",
                Surname = string.Empty,
                IsAdmin = true
            };

            var result = await _sut.TestValidateAsync(request).ConfigureAwait(false);
            result.ShouldHaveValidationErrorFor(request => request.Surname);
        }

        [Fact]
        public async Task Validate_RequestIsValid_ReturnsTrue()
        {
            var request = new CreatePersonRequest
            {
                Forename = "Simon",
                Surname = "Price",
                IsAdmin = true
            };

            var result = await _sut.TestValidateAsync(request).ConfigureAwait(false);
            result.ShouldNotHaveValidationErrorFor(request => request.Surname);
        }

        private CreatePersonValidator CreateSut()
        {
            return new CreatePersonValidator();
        }
    }
}