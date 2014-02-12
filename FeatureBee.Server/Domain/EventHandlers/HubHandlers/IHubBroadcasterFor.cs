namespace FeatureBee.Server.Domain.EventHandlers.HubHandlers
{
    using System;

    public interface IHubBroadcasterFor
    {
        Type ForType { get; }

        void Broadcast(object eventBody);
    }
}