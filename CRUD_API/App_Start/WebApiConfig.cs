using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Owin;


namespace CRUD_API
{
    public static class WebApiConfig
    {
        static dynamic key = Encoding.ASCII.GetBytes("your_secret_key_here");
        static dynamic signingKey = new SymmetricSecurityKey(key);
        public static void Register(HttpConfiguration config)
        {
           
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnableCors();
            
        }
        public static void ConfigureAuth(IAppBuilder config)
        {
           
            config.UseJwtBearerAuthentication(
                    new JwtBearerAuthenticationOptions
                    {
                        AuthenticationMode = AuthenticationMode.Active,
                        TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        }
                    });
            ConfigureAuth(config);

        }
    }
}

