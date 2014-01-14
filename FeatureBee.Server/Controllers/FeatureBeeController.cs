namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using FeatureBee.Server.Models;

    public class FeatureBeeController : Controller
    {
        public ActionResult Index()
        {
            return View(new FeatureBeeEnvironmentViewModel { Teams = new List<string> { "Dealer", "ASM Garage" }});
        }
    }
}
