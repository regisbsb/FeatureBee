namespace FeatureBee.Server.Domain.EventHandlers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.Server.Domain.EventHandlers.HubHandlers;
    using FeatureBee.Server.Domain.Infrastruture;

    using NEventStore;

    public class HubEventHandler : IEventHandler
    {
        private readonly IEnumerable<IHubBroadcasterFor> hubBroadcaster;

        public HubEventHandler(IEnumerable<IHubBroadcasterFor> hubBroadcaster)
        {
            this.hubBroadcaster = hubBroadcaster;
        }

        public void Handle(EventMessage @event)
        {
            var hubBroadcasterFor = this.hubBroadcaster.FirstOrDefault(_ => _.ForType == @event.Body.GetType());
            if (hubBroadcasterFor != null)
            {
                hubBroadcasterFor.Broadcast(@event.Body);
            }
            else
            {
                Debug.WriteLine("No broadcaster found for event {0}", @event.GetType());
            }
        }
    }
}