using GrowFood.Domain.Shared;
using GrowFood.Infrastructure.Data;

namespace GrowFood.Infrastructure.EventDispatcher
{
    public interface IHandleEvent<in T> where T : DomainEvent
    {
        void Handle(T @event, GrowFoodDbContext db);
    }
}
