namespace FeatureBee.Server.Domain.EventHandlers.HistoryHandlers
{
    using System;

    using FeatureBee.Server.Models;

    using NEventStore;

    abstract class HistoryBroadcasterFor<T> : IHistoryBroadcasterFor
    {
        public Type ForType
        {
            get
            {
                return typeof(T);
            }
        }

        public abstract void Broadcast(FeatureBeeContext context, EventMessage eventMessage);
    }
}