using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Application.Queries.Persons
{
    public class GetPerson : IRequestHandler<GetPersonRequest, GetPersonResponse>
    {
        private readonly IReadOnlyDbContext _databaseContext;
        private readonly IMapper _mapper;

        public GetPerson(IReadOnlyDbContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<GetPersonResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _databaseContext.Persons
                .FirstOrDefaultAsync(person => person.Id == request.Id, cancellationToken)
                .ConfigureAwait(false);

            var mappedResult = _mapper.Map<GetPersonResponse>(person);
            return mappedResult;
        }
    }
}