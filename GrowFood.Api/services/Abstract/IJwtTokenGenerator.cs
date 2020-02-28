using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowFood.Api.services.Abstract
{
    public interface IJwtTokenGenerator
    {
        string Generate(LoginQueryResponse userInfo);
    }
}
