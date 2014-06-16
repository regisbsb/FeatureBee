using System.Linq;
using System.Web;
using FeatureBee.WireUp;

namespace FeatureBee.Evaluators
{
    using System;

    public class BrowserConditionEvaluator : IConditionEvaluator<WebApplicationContext>
    {
        public Func<HttpContextBase> CurrentContext = () => new HttpContextWrapper(HttpContext.Current);

        public string Name { get { return "browser"; } }
        public bool IsFulfilled(string[] values)
        {
            var currentContext = CurrentContext();

            var shouldBeBrowser = values.Where(_ => !_.Contains(":"));
            var shouldNotBeBrowser = values.Where(_ => _.StartsWith("NOT:")).Select(_ => _.Substring(3));
            return currentContext != null &&
                   shouldBeBrowser.Any(condition => currentContext.Request.Browser.Browser.ToLowerInvariant().Contains(condition.ToLowerInvariant())) &&
                   !shouldNotBeBrowser.Any(condition => currentContext.Request.Browser.Browser.ToLowerInvariant().Contains(condition.ToLowerInvariant()));
        }
    }
}