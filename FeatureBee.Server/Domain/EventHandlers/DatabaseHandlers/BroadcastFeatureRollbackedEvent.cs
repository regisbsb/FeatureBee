namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureRollbackedEvent : DatabaseBroadcasterFor<FeatureRollbackedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureRollbackedEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Index = 0;
            feature.State = "In Development";
        }
    }
}