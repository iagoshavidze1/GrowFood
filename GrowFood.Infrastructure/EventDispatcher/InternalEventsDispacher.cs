using GrowFood.Domain.Shared;
using GrowFood.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace GrowFood.Infrastructure.EventDispatcher
{

    public class InternalEventsDispacher : IInternalEventDispatcher<DomainEvent>
    {
        private readonly IDictionary<Type, List<Type>> _mappHandlers;
        private IServiceProvider _serviceProvider;



        public InternalEventsDispacher(IServiceProvider serviceProvider, Assembly eventsAssembly, params Assembly[] eventHandlersAssembly)
        {
            _mappHandlers = MapEventAndHandlers.GetMappedEventAndHandlers<IHandleEvent<DomainEvent>>(eventsAssembly, eventHandlersAssembly);

            _serviceProvider = serviceProvider;
        }

        private void Dispatch(dynamic _event, GrowFoodDbContext db)
        {
            if (_mappHandlers.ContainsKey(_event.GetType()))
            {
                var handlers = _mappHandlers[_event.GetType()];
                foreach (var handler in handlers)
                {
                    var service = _serviceProvider.GetService(handler);
                    if (service == null)
                    {
                        service = Activator.CreateInstance(handler);
                    }

                    var eventHandler = service as dynamic;
                    eventHandler.Handle(_event, db);
                }
            }
        }

        public void Dispatch(IReadOnlyList<DomainEvent> domainEvents, GrowFoodDbContext dbContext)
        {
            foreach (var item in domainEvents)
            {
                this.Dispatch(item, dbContext);
            }
        }
    }
}
