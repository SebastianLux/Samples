using System.Web.Http;
using Owin;
using OWINSelfHost.Api.Authentication;

namespace OWINSelfHost.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            //config.MessageHandlers.Add(new EnrichingHandler());
            //config.AddResponseEnrichers(new CustomerResponseEnricher());

            appBuilder.Use(typeof (DummyAuthenticationMiddleware), new DummyAuthenticationOptions("Basic"));

            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //); 
  
            appBuilder.UseWebApi(config);
        }
    }
}