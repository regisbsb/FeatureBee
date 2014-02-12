namespace FeatureBee.Server.Domain.EventHandlers.HubHandlers
{
    using System;

    abstract class HubBroadcasterFor<T> : IHubBroadcasterFor
    {
        public Type ForType
        {
            get
            {
                return typeof(T);
            }
        }

        public abstract void Broadcast(object eventBody);
    }
}