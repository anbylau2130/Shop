using System.Web.Mvc;

namespace USP.Areas.ShoppingMall
{
    public class ShoppingMallAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShoppingMall";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShoppingMall_default",
                "ShoppingMall/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}