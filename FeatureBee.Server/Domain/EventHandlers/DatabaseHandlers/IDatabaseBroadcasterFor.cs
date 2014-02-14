namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using System;

    using FeatureBee.Server.Models;

    public interface IDatabaseBroadcasterFor
    {
        Type ForType { get; }

        void Broadcast(FeatureBeeContext featureBeeContext, object eventBody);
    }
}