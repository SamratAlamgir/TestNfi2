using System;
using NFI.Enums;

namespace NFI.Models
{
    public static class DetailViewNames
    {
        public static string ViewName(ApplicationType applicationType)
        {
            string viewName;
            switch (applicationType)
            {
                case ApplicationType.Sørfond:
                    viewName = "../Admin/Sorfond/Details";
                    break;
                case ApplicationType.Insentivordning:
                    viewName = "../Admin/InsentivordningDetail";
                    break;
                case ApplicationType.IncentiveScheme:
                    viewName = "../Admin/IncentiveSchemeDetail";
                    break;

                case ApplicationType.UdsReisestotte:
                    viewName = "../Admin/UdsReisestotteDetail";
                    break;
                case ApplicationType.Lansering:
                    viewName = "../Admin/LanseringDetail";
                    break;
                case ApplicationType.Ordninger:
                    viewName = "../Admin/OrdningerDetail";
                    break;
                case ApplicationType.Film:
                    viewName = "../Admin/FilmDetail";
                    break;
                case ApplicationType.Video:
                    viewName = "../Admin/VideoDetail";
                    break;
                default:
                    throw new Exception("No application detail view created for the application " + applicationType);

            }
            return viewName;
        }
    }
}