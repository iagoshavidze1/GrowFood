using GrowFood.Api.services.Abstract;
using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using GrowFood.Domain.UserAggregate;
using GrowFood.Infrastructure.Data;
using GrowFood.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GrowFood.Api.services.Concrete
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        IConfiguration _config;
        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string Generate(LoginQueryResponse userInfo)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, userInfo.Email));
            claims.Add(new Claim(ClaimConstants.Id, userInfo.Id.ToString()));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"], expires: DateTime.Now.AddHours(12), signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
