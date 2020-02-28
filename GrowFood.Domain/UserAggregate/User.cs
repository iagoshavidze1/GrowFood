using GrowFood.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Domain.UserAggregate
{
    public class User : AggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Guid? Token { get; set; }

        public DateTime? TokenExpireDate { get; set; }
    }
}
