namespace FeatureBee.Server.Domain.EventHandlers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Models;

    using NEventStore;

    public class DatabaseEventHandler : IEventHandler
    {
        private readonly IEnumerable<IDatabaseBroadcasterFor> databaseBroadcaster;

        public DatabaseEventHandler(IEnumerable<IDatabaseBroadcasterFor> databaseBroadcaster)
        {
            this.databaseBroadcaster = databaseBroadcaster;
        }

        public void Handle(EventMessage @event)
        {
            var context = new FeatureBeeContext();
            
            var databaseBroadcasterFor = this.databaseBroadcaster.FirstOrDefault(_ => _.ForType == @event.Body.GetType());
            if (databaseBroadcasterFor != null)
            {
                databaseBroadcasterFor.Broadcast(context, @event.Body);
            }
            else
            {
                Debug.WriteLine("No broadcaster found for event {0}", @event.GetType());
            }

            context.SaveChanges();
        }
    }
}