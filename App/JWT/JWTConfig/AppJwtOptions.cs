using App.JWT.JWTConfig;
using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Owin.Security;

namespace App.IoCConfig
{
    public class AppJwtOptions : JwtBearerAuthenticationOptions
    {
        public AppJwtOptions(IAppJwtConfiguration config)
        {
            this.AuthenticationMode = AuthenticationMode.Active;
            this.AllowedAudiences = new[] { config.JwtAudience };
            this.IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(
                    issuer: config.JwtIssuer,
                    base64Key: Convert.ToBase64String(Encoding.UTF8.GetBytes(config.JwtKey)))
            };
        }
    }
}