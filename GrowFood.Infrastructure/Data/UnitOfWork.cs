using GrowFood.Domain.Shared;
using GrowFood.Infrastructure.EventDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrowFood.Infrastructure.Data
{
    public class UnitOfWork
    {
        GrowFoodDbContext _db;
        InternalEventsDispacher _internalEventsDispacher;
        public UnitOfWork(GrowFoodDbContext context, InternalEventsDispacher eventsDispacher)
        {
            _db = context;
            _internalEventsDispacher = eventsDispacher;
        }

        public void Save()
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                var entries = _db.ChangeTracker.Entries<IHasDomainEvents>().ToList();

                foreach (var entr in entries)
                {
                    var events = entr.Entity.GetUnCommitedEvents();
                    if (events.Any())
                    {
                        _internalEventsDispacher.Dispatch(events, _db);

                        entr.Entity.MarkEventsAsCommited();
                    }
                }

                _db.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
