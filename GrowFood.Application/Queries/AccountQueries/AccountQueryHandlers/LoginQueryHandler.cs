using GrowFood.Application.Queries.AccountQueries.QueryResponses;
using GrowFood.Domain.UserAggregate;
using GrowFood.Domain.UserAggregate.Events;
using GrowFood.Infrastructure.Data;
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
        private GrowFoodDbContext _db;
        private UnitOfWork _unitOfWork;

        public LoginQueryHandler(GrowFoodDbContext db, UnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public Task<LoginQueryResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = new User();

            user.Raise(new UserTestEvent() { User = user });

            _db.Add(user);

            _unitOfWork.Save();

            return Task.FromResult(new LoginQueryResponse());
        }
    }
}
