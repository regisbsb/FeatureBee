namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureLinkedToTicketEvent : DatabaseBroadcasterFor<FeatureLinkedToTicketEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureLinkedToTicketEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Link = body.Link;
        }
    }
}