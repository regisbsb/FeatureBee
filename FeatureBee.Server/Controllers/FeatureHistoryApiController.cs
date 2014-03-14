namespace FeatureBee.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using FeatureBee.Server.Models;

    [AllowAnonymous]
    public class FeatureHistoryApiController : ApiController
    {
        private readonly FeatureBeeContext context;

        public FeatureHistoryApiController()
        {
            context = new FeatureBeeContext();
        }

        [Route("api/features/{featureName}/history")]
        public IQueryable<FeatureHistoryViewModel> Get(string featureName)
        {
            return context.FeatureHistory.Where(x => x.Name.Equals(featureName, StringComparison.InvariantCultureIgnoreCase));
        } 
    }
}