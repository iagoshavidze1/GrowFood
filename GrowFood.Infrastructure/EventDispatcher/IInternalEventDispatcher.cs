using GrowFood.Infrastructure.Data;
using System.Collections.Generic;

namespace GrowFood.Infrastructure.EventDispatcher
{
    public interface IInternalEventDispatcher<TDomainEvent>
    {
        void Dispatch(IReadOnlyList<TDomainEvent> domainEvents, GrowFoodDbContext dbContext);
    }
}
