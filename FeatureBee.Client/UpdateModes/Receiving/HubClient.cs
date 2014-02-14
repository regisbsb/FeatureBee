namespace FeatureBee.UpdateModes.Receiving
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNet.SignalR.Client;

    class HubClient : ICanReceiveFeatureStates
    {
        private HubConnection connection;

        private IHubProxy hub;

        public ICanReceiveFeatureStates Open(string host, string endPoint)
        {
            this.connection = new HubConnection(host);
            //Make proxy to hub based on hub name on server
            this.hub = this.connection.CreateHubProxy(endPoint);
            this.connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.WriteLine("There was an error opening the connection:{0}",
                        task.Exception.GetBaseException());
                }
                else
                {
                    Debug.WriteLine("Connected");
                }

            }).Wait();

            return this;
        }

        public ICanReceiveFeatureStates On<T>(string @event, Action<T> callback)
        {
            this.hub.On(@event, callback);
            return this;
        }

        public void Dispose()
        {
            this.connection.Stop();
        }
    }
}