namespace FeatureBee
{
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    using FeatureBee.WireUp;

    public static class Tray
    {
        public static HtmlString RenderIcon()
        {
            if (!FeatureBeeBuilder.Context.ShowTrayIconOnPages)
            {
                return new HtmlString(string.Empty);
            }

            var full = new StringBuilder();
            full.AppendFormat("<style type=\"text/css\">{0}</style>", LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.css"));
            full.Append(LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.html"));
            full.AppendFormat(
                "<script type\"text/javascript\">{0}</script>",
                LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.js"));

            return new HtmlString(full.ToString());
        }

        private static string LoadResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}