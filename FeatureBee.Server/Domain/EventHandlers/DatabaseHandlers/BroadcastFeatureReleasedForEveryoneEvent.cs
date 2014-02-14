namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureReleasedForEveryoneEvent : DatabaseBroadcasterFor<FeatureReleasedForEveryoneEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureReleasedForEveryoneEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Index = 2;
            feature.State = "Released";
        }
    }
}