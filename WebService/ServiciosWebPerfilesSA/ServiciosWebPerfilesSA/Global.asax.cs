using System.Web.Http;

namespace ServiciosWebPerfilesSA
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register(); // <-- Esto activa Swagger
        }

    }
}
