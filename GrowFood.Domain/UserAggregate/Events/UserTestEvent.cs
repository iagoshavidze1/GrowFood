using GrowFood.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Domain.UserAggregate.Events
{
    public class UserTestEvent : DomainEvent
    {
        public User User { get; set; }
    }
}
