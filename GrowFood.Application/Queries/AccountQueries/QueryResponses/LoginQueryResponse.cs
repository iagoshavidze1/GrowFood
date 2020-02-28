using GrowFood.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Application.Queries.AccountQueries.QueryResponses
{
    public class LoginQueryResponse : QueryResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }
    }
}
