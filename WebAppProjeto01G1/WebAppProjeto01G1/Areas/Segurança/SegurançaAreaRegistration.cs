using System.Web.Mvc;

namespace WebAppProjeto01G1.Areas.Segurança
{
    public class SegurançaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Segurança";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Segurança_default",
                "Segurança/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}