namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    class BroadcastFeatureReleasedForEveryoneEvent : HistoryBroadcasterFor<FeatureReleasedForEveryoneEvent>
    {
        public override void Broadcast(FeatureBeeContext context, EventMessage eventMessage)
        {
            context.FeatureHistory.Add(new FeatureHistoryViewModel
            {
                Name = (eventMessage.Body as FeatureReleasedForEveryoneEvent).Name,
                Action = "ChangedState",
                Payload = "Released",
                Date = DateTime.Now,
                UserId = eventMessage.Headers["UserId"].ToString()
            });
        }
    }
}