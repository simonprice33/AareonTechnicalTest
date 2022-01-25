using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries.Persons;
using AutoMapper;

namespace AareonTechnicalTest.Application.MappingProfiles
{
    public class GetPersonProfiles : Profile
    {
        public GetPersonProfiles()
        {
            CreateMap<Person, GetPersonResponse>();
        }
    }
}