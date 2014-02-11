namespace FeatureBee.UpdateModes.Receiving
{
    using System;

    internal interface ICanReceiveFeatureStates : IDisposable
    {
        ICanReceiveFeatureStates Open(string host, string endPoint);

        ICanReceiveFeatureStates On<T>(string @event, Action<T> callback);
    }
}