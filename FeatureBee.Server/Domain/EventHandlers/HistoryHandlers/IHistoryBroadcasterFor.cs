namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Models;

    using NEventStore;

    public interface IHistoryBroadcasterFor
    {
        Type ForType { get; }

        void Broadcast(FeatureBeeContext context, EventMessage eventBody);
    }
}