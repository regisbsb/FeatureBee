namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    class BroadcastFeatureCreatedEvent : DatabaseBroadcasterFor<FeatureCreatedEvent>
    {
        public override void Broadcast(FeatureBeeContext context, object eventBody)
        {
            try
            {
                var body = eventBody as FeatureCreatedEvent;

                context.Features.Add(new FeatureViewModel
                                     {
                                         Id = body.AggregateId,
                                         Name = body.Name,
                                         Description = body.Description,
                                         Team = body.Team,
                                         Index = 0,
                                         State = "In Development",
                                         Conditions = body.Conditions.Select(ToConditionViewModel).ToList()
                                     });
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }
    }
}