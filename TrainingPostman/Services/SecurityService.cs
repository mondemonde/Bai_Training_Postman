using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TrainingPostman.Services
{
    public class SecurityService
    {
        public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
        {
            public override void OnAuthorization(HttpActionContext actionContext)
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    string authenticationToken = actionContext.Request.Headers
                                                .Authorization.Parameter;
                    string decodedAuthenticationToken = Encoding.UTF8.GetString(
                        Convert.FromBase64String(authenticationToken));
                    string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                    string username = usernamePasswordArray[0];
                    string password = usernamePasswordArray[1];

                    if (UserSecurity.Login(username, password))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(
                            new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
            }
        }

        public class UserSecurity
        {
            public static bool Login(string username, string password)
            {
                return username == "admin" && password == "123";
            }
        }
    }
}