using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace OWINSelfHost.Api.Authentication
{
    class DummyAuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult(new AuthenticationTicket(BasicHttpAuthenticator.Authenticate(Request), null));
        }
    }
}
