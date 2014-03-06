namespace FeatureBee.HttpHandlerRouting
{
    using System.Net;
    using System.Web;

    public class NoRouteFound : IHandleARoute
    {
        public bool CanHandleRoute(string url)
        {
            return true;
        }

        public void DoHandleRoute(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
        }
    }
}