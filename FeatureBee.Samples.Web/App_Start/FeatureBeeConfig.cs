namespace FeatureBee.Samples.Web
{
    using System;
    using System.Diagnostics;
    using System.Web;

    using FeatureBee.WireUp;

    public static class FeatureBeeConfig
    {
        public static void InitFor(HttpApplication app)
        {
            FeatureBeeBuilder
                .ForWebApp()
                .UseConfig()
                .LogTo(Logger());
        }

        private static Action<TraceEventType, string> Logger()
        {
            return (eventType, message) => Trace.WriteLine(eventType + ": " + message);
        }
    }
}