namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    class BroadcastFeatureRollbackedEvent : HistoryBroadcasterFor<FeatureRollbackedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, EventMessage eventMessage)
        {
            context.FeatureHistory.Add(new FeatureHistoryViewModel
                                       {
                                           Name = (eventMessage.Body as FeatureRollbackedEvent).Name,
                                           Action = "ChangedState",
                                           Payload = "Under Development",
                                           Date = DateTime.Now,
                                           UserId = eventMessage.Headers["UserId"].ToString()
                                       });
        }
    }
}