using System.Web.Mvc;

namespace MageWarsWebSite.Web.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_lang",
                "{lang}/User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "en" },
                new {lang = @"en|ru|uk"}
            );
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "en" }
            );
        }
    }
}