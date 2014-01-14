namespace FeatureBee.Server.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using FeatureBee.Server.ConfigSection;
    using FeatureBee.Server.Models;

    public class FeatureBeeController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeatureBeeEnvironmentViewModel { Teams = FeatureBeeConfiguration.GetSection().Teams.ToList().Select(_ => _.Name).ToList()});
        }
    }
}
