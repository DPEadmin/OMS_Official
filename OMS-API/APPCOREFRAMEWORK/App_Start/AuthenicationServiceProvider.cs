using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using APPCOREMODEL.Datas.Authens;
using APPCOREMODEL.Services.authens;

namespace APPCOREVIEW
{
    public class AuthenicationServiceProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            AuthenResult _result = new AuthenicatedServices().authenicated(System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(context.UserName)), System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(context.Password))); 
            if(_result != null)
            {
                List<Emp> resultset = _result.Empployee;

                identity.AddClaim(new Claim(ClaimTypes.Role, resultset[0].EmpRole));
                identity.AddClaim(new Claim(ClaimTypes.GivenName,resultset[0].EmpVendor));
                identity.AddClaim(new Claim(ClaimTypes.Name,resultset[0].EmpUsername));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, resultset[0].EmpCode));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid_grant", "Provider username and password incorrect");
                return;
            }
        }
    }
}