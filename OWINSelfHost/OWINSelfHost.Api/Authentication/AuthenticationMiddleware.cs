using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace OWINSelfHost.Api.Authentication
{
    public class DummyAuthenticationMiddleware : AuthenticationMiddleware<AuthenticationOptions>
    {
        public DummyAuthenticationMiddleware(OwinMiddleware next, AuthenticationOptions options) : base(next, options) { }

        //public override async Task Invoke(IOwinContext context)
        //{
        //    var claimsIdentity = BasicHttpAuthenticator.Authenticate(context.Request);

        //    if (claimsIdentity != null)
        //    {
        //        context.Request.User = new ClaimsPrincipal(claimsIdentity);
        //    }

        //    await Next.Invoke(context);
        //}

        protected override AuthenticationHandler<AuthenticationOptions> CreateHandler()
        {
            return new DummyAuthenticationHandler();
        }
    }
}