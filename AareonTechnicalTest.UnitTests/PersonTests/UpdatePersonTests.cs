using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Commands.Persons.Update;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries.Persons;
using AareonTechnicalTest.Data.Data;
using AareonTechnicalTest.DataHelpers;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AareonTechnicalTest.UnitTests.PersonTests
{
    public class UpdatePersonTests
    {
        private ApplicationContext _databaseContext;
        private readonly IMapper _mapper;
        private Person _person;
        private readonly UpdatePerson _sut;

        public UpdatePersonTests()
        {
            _databaseContext = new DbBuilder(false)
                .AddPerson("Simon", "Price", true, out _person).Build();

            _sut = CreateSut();
        }

        [Theory]
        [InlineData("Rick", "Sanchez", true)]
        [InlineData("Morty", "Sanchez", false)]
        [InlineData("Bird", "Person", false)]
        public async Task UpdatePerson_ReturnsUnit(string forname, string surname, bool isAdmin)
        {
            var request = new UpdatePersonRequest
            {
                Id = 1,
                Forename = forname,
                Surname = surname,
                IsAdmin = isAdmin
            };

            var result = _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbValue = await _databaseContext.Persons
                .FirstOrDefaultAsync(test => test.Id == 1, CancellationToken.None).ConfigureAwait(false);

            dbValue.Forename.Should().Be(forname);
            dbValue.Surname.Should().Be(surname);
            dbValue.IsAdmin.Should().Be(isAdmin);
        }

        [Theory]
        [InlineData("Rick")]
        [InlineData("Morty")]
        [InlineData("Bird")]
        public async Task UpdatePerson_UpdateForename_ReturnsUnit(string forname)
        {
            var request = new UpdatePersonRequest
            {
                Id = 1,
                Forename = forname,
            };

            var result = _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbValue = await _databaseContext.Persons
                .FirstOrDefaultAsync(test => test.Id == 1, CancellationToken.None).ConfigureAwait(false);

            dbValue.Forename.Should().Be(forname);
        }

        [Theory]
        [InlineData("Rick")]
        [InlineData("Morty")]
        [InlineData("Bird")]
        public async Task UpdatePerson_UpdateSurname_ReturnsUnit(string surname)
        {
            var request = new UpdatePersonRequest
            {
                Id = 1,
                Surname = surname
            };

            var result = _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbValue = await _databaseContext.Persons
                .FirstOrDefaultAsync(test => test.Id == 1, CancellationToken.None).ConfigureAwait(false);
            dbValue.Surname.Should().Be(surname);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdatePerson_UpdateAdmin_ReturnsUnit(bool isAdmin)
        {
            var request = new UpdatePersonRequest
            {
                Id = 1,
                IsAdmin = isAdmin
            };

            var result = _sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbValue = await _databaseContext.Persons
                .FirstOrDefaultAsync(test => test.Id == 1, CancellationToken.None).ConfigureAwait(false);

            dbValue.IsAdmin.Should().Be(isAdmin);
        }

        private UpdatePerson CreateSut()
        {
            return new UpdatePerson(_databaseContext);
        }
    }
}