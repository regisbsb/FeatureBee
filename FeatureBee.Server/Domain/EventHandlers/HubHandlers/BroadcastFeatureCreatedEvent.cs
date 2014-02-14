namespace FeatureBee.Server.Domain.EventHandlers.HubHandlers
{
    using FeatureBee.Server.Controllers;
    using FeatureBee.Server.Domain.Models;

    using Microsoft.AspNet.SignalR;

    class BroadcastFeatureCreatedEvent : HubBroadcasterFor<FeatureCreatedEvent>
    {
        public override void Broadcast(object eventBody)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
            hub.Clients.All.featureCreated((eventBody as FeatureCreatedEvent).Name);
        }
    }
}