namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;
    using System.Web.Helpers;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    class BroadcastFeatureConditionsUpdatedEvent : HistoryBroadcasterFor<FeatureConditionsUpdatedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, EventMessage eventMessage)
        {
            context.FeatureHistory.Add(new FeatureHistoryViewModel
            {
                Name = (eventMessage.Body as FeatureConditionsUpdatedEvent).Name,
                Action = "ConditionsChanged",
                Payload = Json.Encode((eventMessage.Body as FeatureConditionsUpdatedEvent).Conditions),
                Date = DateTime.Now,
                UserId = eventMessage.Headers["UserId"].ToString()
            });
        }
    }
}