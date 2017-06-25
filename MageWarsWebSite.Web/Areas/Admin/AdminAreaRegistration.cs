using System.Web.Mvc;

namespace MageWarsWebSite.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Lang_default",
                "{lang}/Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "en" },
                new { lang = @"en|uk|ru" }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang="en" }
            );
        }
    }
}