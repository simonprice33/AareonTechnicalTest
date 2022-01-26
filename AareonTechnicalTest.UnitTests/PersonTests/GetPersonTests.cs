using AareonTechnicalTest.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.MappingProfiles;
using AareonTechnicalTest.Application.Queries;
using AareonTechnicalTest.Application.Queries.Persons;
using AareonTechnicalTest.Data.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using AareonTechnicalTest.DataHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class GetPersonTests
    {
        private ApplicationContext databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private readonly GetPerson _sut;

        public GetPersonTests()
        {
            _mapper = new Mapper(new MapperConfiguration(expression =>
                expression.AddProfiles(new List<Profile>()
                {
                    new GetPersonProfiles()
                })
            ));

            databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task GetPerson_Returns_Person()
        {
            var request = new GetPersonRequest
            {
                Id = _person.Id
            };

            var expectedResult = new GetPersonResponse
            {
                Id = 1,
                Forename = "Simon",
                Surname = "Price",
                IsAdmin = true
            };

            var result = await _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            result.Should().NotBeNull().And.BeEquivalentTo(expectedResult);
        }

        private GetPerson CreateSut()
        {
            return new GetPerson(databaseContext, _mapper);
        }
    }
}