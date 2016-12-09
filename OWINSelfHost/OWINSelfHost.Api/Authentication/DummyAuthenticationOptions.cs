using Microsoft.Owin.Security;

namespace OWINSelfHost.Api.Authentication
{
    public class DummyAuthenticationOptions : AuthenticationOptions
    {
        public DummyAuthenticationOptions(string authenticationType) : base(authenticationType)
        {
        }
    }
}
