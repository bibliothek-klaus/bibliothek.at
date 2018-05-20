using bibliothek.at.Contracts;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bibliothek.at
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IEnhanceMedia, AmazonEnhanceMedia>(Lifestyle.Scoped);

            //Mock Data
            //container.Register<IMediaRepository, MockMediaRepository>(Lifestyle.Scoped);
            //container.Register<ICalendarRepository, MockCalendarRepository>(Lifestyle.Scoped);

            //Real Data
            container.Register<IMediaRepository, MySqlMediaRepository>(Lifestyle.Scoped);
            container.Register<ICalendarRepository, GoogleCalendarRepository>(Lifestyle.Scoped);
            container.Register<IEmailMarketing, MailChimpEmailMarketing>(Lifestyle.Scoped);


            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}