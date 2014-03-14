namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    class BroadcastFeatureReleasedWithConditionsEvent : HistoryBroadcasterFor<FeatureReleasedWithConditionsEvent>
    {
        public override void Broadcast(FeatureBeeContext context, EventMessage eventMessage)
        {
            context.FeatureHistory.Add(new FeatureHistoryViewModel
            {
                Name = (eventMessage.Body as FeatureReleasedWithConditionsEvent).Name,
                Action = "ChangedState",
                Payload = "Under Test",
                Date = DateTime.Now,
                UserId = eventMessage.Headers["UserId"].ToString()
            });
        }
    }
}