namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using System.Linq;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureConditionsUpdatedEvent : DatabaseBroadcasterFor<FeatureConditionsUpdatedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            var body = @eventBody as FeatureConditionsUpdatedEvent;
            if (body == null)
            {
                return;
            }

            var feature = context.Features.Find(body.AggregateId);
            feature.Conditions.Clear();
            feature.Conditions = body.Conditions.Select(ToConditionViewModel).ToList();
        }
    }
}