using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowFood.Application.Queries.AccountQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrowFood.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public string Login()
        {
            var query = new LoginQuery();

            var queryResult = _mediator.Send(query).Result;

            return "sd";
        }
    }
}