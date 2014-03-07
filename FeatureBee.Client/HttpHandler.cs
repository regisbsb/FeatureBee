﻿using System.Net;
using System.Web;

namespace FeatureBee
{
    using System.Linq;

    using FeatureBee.HttpHandlerRouting;

    public class HttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var routeHandlers = new IHandleARoute[] { new AllFeatures(), new FeatureState(), new NoRouteFound() }; 

            routeHandlers.First(_ => _.CanHandleRoute(context.Request.Path)).DoHandleRoute(context);
        }

        public bool IsReusable { get { return true; }}
    }
}
