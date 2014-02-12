namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureReleasedWithConditionsEvent : DatabaseBroadcasterFor<FeatureReleasedWithConditionsEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureReleasedWithConditionsEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Index = 1;
            feature.State = "Under Test";
        }
    }
}