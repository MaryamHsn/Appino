using App.JWT.JWTConfig;
using App_Services.JWTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace App.Controller
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly ITokenStoreService _tokenStoreService;

        public UserController(ITokenStoreService tokenStoreService)
        {
            _tokenStoreService = tokenStoreService;
        }
        /// <summary>
        /// Delete Token in server 
        /// </summary>
        /// <returns></returns>
        [JwtAuthorize]
        [Route("logout")]
        [HttpGet, HttpPost]
        public IHttpActionResult Logout()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;

            // The OWIN OAuth implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            _tokenStoreService.InvalidateUserTokens(int.Parse(userId));
            _tokenStoreService.DeleteExpiredTokens();

            return this.Ok(new { message = "Logout successful." });
        }
    }
}