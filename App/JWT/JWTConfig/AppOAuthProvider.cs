using App_Data.UnitOfWork;
using App_Services.JWTServices;
using DataModel.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace App.JWT.JWTConfig
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IUsersService> _usersService;
        private readonly Func<ITokenStoreService> _tokenStoreService;
        private readonly ISecurityService _securityService;
        private readonly IAppJwtConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Using Func here, creates transient Service's in a singleton AppOAuthProvider
        /// </summary>
        public AppOAuthProvider(
            Func<IUsersService> usersService,
            Func<ITokenStoreService> tokenStoreService,
            ISecurityService securityService,
            IAppJwtConfiguration configuration,
            IUnitOfWork  unitOfWork)
        {
            _usersService = usersService;
            _usersService.CheckArgumentNull(nameof(_usersService));

            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentNull(nameof(_tokenStoreService));

            _securityService = securityService;
            _securityService.CheckArgumentNull(nameof(_securityService));

            _configuration = configuration;
            _configuration.CheckArgumentNull(nameof(_configuration));
            _unitOfWork = unitOfWork;
            _unitOfWork.CheckArgumentNull(nameof(_unitOfWork));
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId != null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            // Change authentication ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            var userId = int.Parse(context.Ticket.Identity.FindFirst(ClaimTypes.UserData).Value);
            _usersService().UpdateUserLastActivityDate(userId);

            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {


            
            var user = _usersService().FindUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            var identity = setClaimsIdentity(context, user);
            context.Validated(identity);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            context.Validated();
            return Task.FromResult(0);
        }

        private ClaimsIdentity setClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, UserTbl user)
        {
            var identity = new ClaimsIdentity(authenticationType: "JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));//specialize claim 

            // to invalidate the token
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, user.SerialNumber));

            // custom data
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.UserId.ToString()));

            //var roles = user.Roles;
            //foreach (var role in roles)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, user.Roles));
            //}
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Roles));
            return identity;
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            _tokenStoreService().UpdateUserToken(
                userId: int.Parse(context.Identity.FindFirst(ClaimTypes.UserData).Value),
                accessTokenHash: _securityService.GetSha256Hash(context.AccessToken)
            );

            return base.TokenEndpointResponse(context);
        }
    }
}