using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.Client
{
    public interface IFeatureBeeContext { }

    public class WebApplicationContext: IFeatureBeeContext
    {
        public HttpContext HttpContext { get; private set; }

        public WebApplicationContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }

    public static class FeatureBeeConfig
    {
        internal static IFeatureBeeContext Context { get; private set; }
        public static void SetContext<T>(T app)
        {
            Context = new WebApplicationContext(app.Context);
            Conditions = LoadConditions(app);
        }
    }

    public static class FeatureBee
    {
        public static bool IsToggleEnabled(string toggleName)
        {
            // Check if FeatureBeeConfig was initialized
            if (FeatureBeeConfig.Context == null)
            {
                throw new InvalidOperationException("FeatureBeeConfing.SetContext needs to be called first!");
            }

            // Get latest update from server

            // Evaluate conditions
            foreach (var condition in feature.Conditions)
            {
                var evaluator = FeatureBeeConfig.ConditionEvaluators.Where(x => x.Name = condition.Name);
                evaluator.IsFulfilled(FeatureBeeConfig.Context, condition.Values);
            }

        }
    }

    public class Feature
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }
    }

    public class Condition
    {
        public string Evaluator { get; set; }
        public object Value { get; set; }
    }
}
