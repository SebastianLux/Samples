using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin;

namespace OWINSelfHost.Api.Authentication
{
    public static class BasicHttpAuthenticator
    {
        public static ClaimsIdentity Authenticate(IOwinRequest request)
        {
            var header = request.Headers["Authorization"];

            if (!String.IsNullOrWhiteSpace(header))
            {
                var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

                if ("Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));

                    var parts = parameter.Split(':');
                    var username = parts[0];
                    var password = parts[1];

                    if (username == password)
                    {
                        // http://stackoverflow.com/questions/24892222/using-claims-types-properly-in-owin-identity-and-asp-net-mvc
                        var claims = new[] { new Claim(ClaimTypes.Name, username) };
                        return new ClaimsIdentity(claims, "Basic");
                    }
                }
            }

            return null;
        }
    }
}