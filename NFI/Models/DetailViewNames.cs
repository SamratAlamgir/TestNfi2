using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                case ApplicationType.Sorfond:
                    viewName = "../Admin/Sorfond/Details";
                    break;
                case ApplicationType.Insentivordning:
                    viewName = "../Admin/InsentivordningDetail";
                    break;
                case ApplicationType.IncentiveScheme:
                    viewName = "../Admin/IncentiveSchemeDetail";
                    break;
                case ApplicationType.Lansering:
                    viewName = "../Admin/LanseringDetail";
                    break;
                case ApplicationType.Ordninger:
                    viewName = "../Admin/OrdningerDetail";
                    break;
                default:
                    throw new Exception("No application detail view created for the application " + applicationType);

            }
            return viewName;
        }
    }
}