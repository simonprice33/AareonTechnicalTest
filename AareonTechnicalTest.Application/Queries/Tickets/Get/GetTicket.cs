using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Queries.Tickets.Get
{
    public class GetTicket : IRequestHandler<GetTicketRequest, GetTicketResponse>
    {
        private readonly IReadOnlyDbContext _databaseContext;
        private readonly IMapper _mapper;

        public GetTicket(IReadOnlyDbContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var query = _databaseContext.Tickets
                .Include(person => person.Person)
                .Where(ticket => ticket.Id == request.Id);

            var mappedResult = await _mapper.ProjectTo<GetTicketResponse>(query).FirstOrDefaultAsync();

            return mappedResult ?? new GetTicketResponse();
        }
    }
}