using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using GrowFood.Application.Shared;
using GrowFood.Domain.UserAggregate;
using GrowFood.Domain.UserAggregate.Events;
using GrowFood.Infrastructure.Data;
using GrowFood.Shared.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrowFood.Application.Queries.AccountQueries.AccountQueryHandlers
{
    public class LoginQueryHandler : Query<LoginQueryResponse>, IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        private GrowFoodDbContext _db;
        private UnitOfWork _unitOfWork;
        private IPasswordHasher _passwordHasher;
        public LoginQueryHandler(GrowFoodDbContext db, UnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public Task<LoginQueryResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _db.Set<User>().ToList().SingleOrDefault(x => x.Email == request.Email && _passwordHasher.PasswordMatches(request.Password, x.Password));

            user = new User
            {
                Email = "test",
                Id = 1
            };

            if (user == null)
                return Fail();

            var queryResponse = new LoginQueryResponse();

            queryResponse.Id = user.Id;
            queryResponse.Email = user.Email;

            user.Raise(new UserTestEvent() { User = new User() });

            //_db.Add(user);

            _unitOfWork.Save();

            return Task.FromResult(queryResponse);
        }
    }
}
