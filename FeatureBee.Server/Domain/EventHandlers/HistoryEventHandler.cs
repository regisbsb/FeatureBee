namespace FeatureBee.Server.Domain.EventHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using FeatureBee.Server.Domain.EventHandlers.HistoryHandlers;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Models;

    using NEventStore;

    public class HistoryEventHandler : IEventHandler
    {
        private readonly IEnumerable<IHistoryBroadcasterFor> databaseBroadcaster;

        public HistoryEventHandler(IEnumerable<IHistoryBroadcasterFor> databaseBroadcaster)
        {
            this.databaseBroadcaster = databaseBroadcaster;
        }

        public void Handle(EventMessage @event)
        {
            var databaseBroadcasterFor = this.databaseBroadcaster.FirstOrDefault(_ => _.ForType == @event.Body.GetType());
            if (databaseBroadcasterFor == null) return;

            var context = new FeatureBeeContext();
            databaseBroadcasterFor.Broadcast(context, @event);
            context.SaveChanges();
        }
    }
}