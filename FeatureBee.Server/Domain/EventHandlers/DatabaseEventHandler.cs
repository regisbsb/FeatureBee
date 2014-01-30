namespace FeatureBee.Server.Domain.EventHandlers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    public class DatabaseEventHandler : IEventHandler
    {
        public void Handle(EventMessage @event)
        {
            var context = new FeatureBeeContext();

            if (@event.Body is FeatureCreatedEvent)
            {
                try
                {
                    var body = @event.Body as FeatureCreatedEvent;

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

            var domainEvent = (@event.Body as IDomainEvent);
            if (@event.Body is FeatureDescriptionUpdatedEvent)
            {
                var body = @event.Body as FeatureDescriptionUpdatedEvent;
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Description = body.Description;
            }

            if (@event.Body is FeatureLinkedToTicketEvent)
            {
                var body = @event.Body as FeatureLinkedToTicketEvent;
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Link = body.Link;
            }

            if (@event.Body is FeatureReleasedForEveryoneEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 2;
                feature.State = "Released";
            }

            if (@event.Body is FeatureReleasedWithConditionsEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 1;
                feature.State = "Under Test";
            }

            if (@event.Body is FeatureRollbackedEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 0;
                feature.State = "In Development";
            }

            if (@event.Body is FeatureRemovedEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                context.Features.Remove(feature);
            }

            if (@event.Body is FeatureConditionsUpdatedEvent)
            {
                var body = @event.Body as FeatureConditionsUpdatedEvent;
                var feature = context.Features.Find(body.AggregateId);
                feature.Conditions.Clear();
                feature.Conditions = body.Conditions.Select(ToConditionViewModel).ToList();
            }

            context.SaveChanges();
        }

        private static ConditionViewModel ToConditionViewModel(Condition condition)
        {
            return new ConditionViewModel { Type = condition.Type, Values = new PersistableStringCollection(condition.Values) };
        }
    }
}