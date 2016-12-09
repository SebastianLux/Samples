using System.Collections.Generic;
using System.Web.Http;
using OWINSelfHost.Api.Models;

namespace OWINSelfHost.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class HomeController : ApiController
    {
        [Route]
        public Home Get()
        {
            var index = new Home();

            index.Resources = new List<string>();
            index.Resources.Add(Url.Link("customers", new { }));
            return index;
        }
    }
}