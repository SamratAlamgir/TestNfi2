namespace NFI.Helper
{
    public static class HtmlExtensions
    {
        public static string GetHostServerUrl(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return Properties.Settings.Default.HostServerUrl;
        }

        public static string GetWaitMessageForApplication(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return "Din søknad er i gang. Det kan ta flere minutter å fullføre, avhengig av dine filstørrelser. Vennligst vent...";
        }
    }
}