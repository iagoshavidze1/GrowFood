using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Domain.Shared
{
    public class AggregateRoot
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public EntityStatus EntityStatus { get; set; }

        public void Deactivate() => EntityStatus = EntityStatus.Deactivated;

    }
}
