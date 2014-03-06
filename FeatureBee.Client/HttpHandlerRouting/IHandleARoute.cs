namespace FeatureBee.HttpHandlerRouting
{
    using System.Web;

    public interface IHandleARoute
    {
        bool CanHandleRoute(string url);

        void DoHandleRoute(HttpContext context);
    }
}