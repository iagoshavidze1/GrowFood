using GrowFood.Domain.UserAggregate.Events;
using GrowFood.Infrastructure.Data;
using GrowFood.Infrastructure.EventDispatcher;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Application.EventHandlers
{
    public class UserCreatedEventHandler : IHandleEvent<UserTestEvent>
    {
        public void Handle(UserTestEvent @event, GrowFoodDbContext db)
        {
            var t = @event;
        }
    }
}
