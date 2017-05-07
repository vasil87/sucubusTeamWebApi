namespace WebAPI
{
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DbConfig.Initiliaze();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
