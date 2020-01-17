using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TokenAuthenticationWebAPI.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //return base.ValidateClientAuthentication(context);
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //return base.GrantResourceOwnerCredentials(context);
            UserMasterRepository _repo = new UserMasterRepository();

            var user = _repo.validateUser(context.UserName, context.Password);

            if(user == null)
            {
                context.SetError("invalid_grant", "Provided user name and passsword is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            
            string[] Roles = user.UserRoles.Split(',');
            
            foreach(string item in Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, item));
            }

            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("Email", user.UserEmailID));

            context.Validated(identity);
        }
    }
}