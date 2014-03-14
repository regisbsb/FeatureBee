namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    class BroadcastFeatureRemovedEvent : HistoryBroadcasterFor<FeatureRemovedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, EventMessage eventMessage)
        {
            context.FeatureHistory.Add(new FeatureHistoryViewModel
                                       {
                                           Name = (eventMessage.Body as FeatureRemovedEvent).Name,
                                           Action = "Removed",
                                           Payload = "",
                                           Date = DateTime.Now,
                                           UserId = eventMessage.Headers["UserId"].ToString()
                                       });
        }
    }
}