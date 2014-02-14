namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureDescriptionUpdatedEvent : DatabaseBroadcasterFor<FeatureDescriptionUpdatedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureDescriptionUpdatedEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Description = body.Description;
        }
    }
}