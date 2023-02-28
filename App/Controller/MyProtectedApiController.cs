using App.JWT.JWTConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace App.Controller
{
    [JwtAuthorize]
    public class MyProtectedApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Title = "Hello from My Protected Controller!",
                Username = this.User.Identity.Name
            });
        }
    }
}