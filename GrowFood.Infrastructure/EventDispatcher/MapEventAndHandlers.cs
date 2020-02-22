using GrowFood.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GrowFood.Infrastructure.EventDispatcher
{
    public class MapEventAndHandlers
    {
        private IDictionary<Type, List<Type>> _mapper = new Dictionary<Type, List<Type>>();

        public static IDictionary<Type, List<Type>> GetMappedEventAndHandlers<THandler>(Assembly eventAssembly, Assembly[] eventHandlersAssembly)
        {
            var result = new Dictionary<Type, List<Type>>();

            var eventTypes = eventAssembly.GetTypes().Where(t => typeof(DomainEvent).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsInterface);

            foreach (var eventType in eventTypes)
            {
                result[eventType] = new List<Type>();

                var probablyHandlerServices = eventHandlersAssembly.SelectMany(x => x.GetTypes());
                foreach (var probablyHandlerService in probablyHandlerServices)
                {
                    var handleInterfaces = probablyHandlerService.GetInterfaces()
                        .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(THandler).GetGenericTypeDefinition());

                    if (handleInterfaces.Any())
                    {
                        foreach (var handleInterface in handleInterfaces)
                        {
                            var genericType = handleInterface.GetGenericArguments().FirstOrDefault(t => t == eventType);

                            if (genericType != null)
                            {
                                result[eventType].Add(probablyHandlerService);
                            }
                        }
                    }
                }
            }


            return result;
        }
    }
}
