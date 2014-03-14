using System.Web;
using FeatureBee.WireUp;

namespace FeatureBee.Samples.Web
{
    using System.Diagnostics;

    public static class FeatureBeeConfig
    {
        public static void InitFor(HttpApplication app)
        {
            FeatureBeeBuilder.ForWebApp().UseConfig();
            //FeatureBeeBuilder.ForWebApp().UseConfig().LogTo((TraceEventType eventType, string message) =>
            //{
            //    Trace.WriteLine(eventType + ": " + message);
            //} );
        }
    }
}