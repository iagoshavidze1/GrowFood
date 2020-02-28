using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrowFood.Api.services.Abstract;
using GrowFood.Application.Queries.AccountQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrowFood.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;
        IJwtTokenGenerator _jwtTokenGenerator;
        public AccountController(IMediator mediator, IJwtTokenGenerator jwtTokenGenerator)
        {
            _mediator = mediator;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginQuery loginQuery)
        {
            var res = await _mediator.Send(loginQuery);

            var token = _jwtTokenGenerator.Generate(res);

            return Ok(new { token = token });
        }

        [HttpGet]
        [Authorize]
        [Route("Test")]
        public IActionResult Test(int id)
        {
            var t = User.Identity.Name;
            var claim = User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Email);

            var i = id;

            return Ok();
        }
    }
}