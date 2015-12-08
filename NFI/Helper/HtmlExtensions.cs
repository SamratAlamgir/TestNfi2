namespace NFI.Helper
{
    public static class HtmlExtensions
    {
        public static string GetHostServerUrl(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return Properties.Settings.Default.HostServerUrl;
        }
    }
}