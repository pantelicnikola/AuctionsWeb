using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AuctionsWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(connectionString);
        }
    }
}
