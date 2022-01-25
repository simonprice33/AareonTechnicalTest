using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Delete;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class DeletePersonTests
    {
        private ApplicationContext _databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private readonly DeletePerson _sut;

        public DeletePersonTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();

            _sut = CreateSut();
        }

        [Fact]
        public async Task DeletePerson_ReturnsUnit()
        {
            var request = new DeletePersonRequest
            {
                Id = 1
            };

            _databaseContext.Persons.ToList().Should().HaveCount(1);
            _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _databaseContext.Persons.ToList().Should().HaveCount(0);
        }

        private DeletePerson CreateSut()
        {
            return new DeletePerson(_databaseContext);
        }
    }
}