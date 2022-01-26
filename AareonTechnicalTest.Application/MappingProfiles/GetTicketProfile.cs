using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Application.Entities;
using AareonTechnicalTest.Application.Queries.Tickets.Get;
using AutoMapper;

namespace AareonTechnicalTest.Application.MappingProfiles
{
    public class GetTicketProfile : Profile
    {
        public GetTicketProfile()
        {
            CreateMap<Ticket, GetTicketResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.Person.Forename ?? string.Empty} {src.Person.Surname ?? string.Empty}"));
        }
    }
}