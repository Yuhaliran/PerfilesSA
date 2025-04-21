using System.Web.Http;
using Swashbuckle.Application;

namespace ServiciosWebPerfilesSA
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ServiciosWebPerfilesSA");
                })
                .EnableSwaggerUi(); 
        }
    }
}
