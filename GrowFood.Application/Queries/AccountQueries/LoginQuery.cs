using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Application.Queries.AccountQueries
{
    public class LoginQuery : IRequest<LoginQueryResponse>
    {
    }
}
