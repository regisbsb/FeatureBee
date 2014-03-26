namespace FeatureBee.WireUp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web;

    using FeatureBee.ConfigSection;
    using FeatureBee.Evaluators;
    using FeatureBee.UpdateModes;

    public class FeatureBeeBuilder
    {
        private FeatureBeeBuilder(IFeatureBeeContext context)
        {
            Context = context;
            Context.Evaluators = LoadConditionEvaluators();
        }

        internal static IFeatureBeeContext Context { get; private set; }

        public static FeatureBeeBuilder ForWebApp(Func<HttpContextBase> httpContextFunc)
        {
            return new FeatureBeeBuilder(new WebApplicationContext(httpContextFunc));
        }

        public static FeatureBeeBuilder ForWebApp()
        {
            return new FeatureBeeBuilder(new WebApplicationContext(() => new HttpContextWrapper(HttpContext.Current)));
        }

        public static FeatureBeeBuilder ForWindowsService()
        {
            return new FeatureBeeBuilder(new WindowsApplicationContext());
        }

        public void Use(IFeatureRepository featureRepository = null, List<IConditionEvaluator> conditionEvaluators = null)
        {
            if (featureRepository != null)
            {
                Context.FeatureRepository = featureRepository;
            }

            if (conditionEvaluators != null)
            {
                Context.Evaluators = conditionEvaluators;
            }
        }

        public FeatureBeeBuilder UseConfig()
        {
            var config = FeatureBeeConfiguration.GetSection();
            Context.FeatureRepository = UpdateModeFactory.Get(config.Server.UpdateMode, config.Server.Url);

            Context.ShowTrayIconOnPages = config.Tray.ShowTrayIconOnPages;
            Context.TrafficDistributionCookie = config.Settings.TrafficDistributionCookie;
            return this;
        }

        private static List<IConditionEvaluator> LoadConditionEvaluators()
        {
            var conditionEvaluators = new List<IConditionEvaluator>();

            try
            {
                Logger.Log(TraceEventType.Verbose, "Load condition evaluators");
                var types = typeof(FeatureBeeBuilder).Assembly.GetTypes().Where(TypeIsConditionEvaluator).ToList();
                conditionEvaluators = types.Select(_ =>
                                                       {
                                                           var constructor = _.GetConstructor(Type.EmptyTypes);
                                                           return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
                                                       }).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    if (exSub is FileNotFoundException)
                    {
                        var exFileNotFound = exSub as FileNotFoundException;
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
               
                Logger.Log(TraceEventType.Error, "Conditions could not be loaded.\r\n" + sb);
            }

            Logger.Log(TraceEventType.Verbose, "Found condition evaluators: " + string.Join(", ", conditionEvaluators.Select(x => x.Name)));
            return conditionEvaluators;
        }

        private static bool TypeIsConditionEvaluator(Type type)
        {
            return type.GetInterface(typeof (IConditionEvaluator).Name) != null && !type.IsAbstract;
        }

        public void LogTo(Action<TraceEventType, string> action)
        {
            Logger.SetLogger(action);
        }
    }

    internal static class Logger
    {
        private static Action<TraceEventType, string> logAction = (eventType, message) => Trace.Write(eventType + ": " + message);

        public static void SetLogger(Action<TraceEventType, string> action)
        {
            logAction = action;
        }

        public static void Log(TraceEventType eventType, string message)
        {
            logAction(eventType, message);
        }

        public static void Log(TraceEventType eventType, string message, params object[] args)
        {
            logAction(eventType, string.Format(message, args));
        }
    }
}