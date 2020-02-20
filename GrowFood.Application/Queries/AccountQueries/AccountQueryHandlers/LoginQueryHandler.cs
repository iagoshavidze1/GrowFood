using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrowFood.Application.Queries.AccountQueries.AccountQueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        public Task<LoginQueryResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new LoginQueryResponse());
        }
    }
}
