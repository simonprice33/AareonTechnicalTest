using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Add;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class CreatePersonTests
    {
        private readonly ApplicationContext _databaseContext;

        private readonly CreatePerson _sut;

        public CreatePersonTests()
        {
            _databaseContext = new DbBuilder(false).Build();
            _sut = CreateSut();
        }

        [Fact]
        public async Task CreatePerson_ReturnsCreatePersonResponse()
        {
            var request = new CreatePersonRequest
            {
                Forename = "Simon",
                Surname = "Price",
                IsAdmin = true
            };

            _databaseContext.Persons.ToList().Should().HaveCount(0);

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            result.Id.Should().Be(1);
        }

        private CreatePerson CreateSut()
        {
            return new CreatePerson(_databaseContext);
        }
    }
}