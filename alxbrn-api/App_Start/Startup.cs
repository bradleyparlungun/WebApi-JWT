using alxbrn_api.Constraints;
using alxbrn_api.Data;
using alxbrn_api.ExceptionHandlers;
using alxbrn_api.Filters;
using alxbrn_api.Loggers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;

[assembly: OwinStartup(typeof(alxbrn_api.Startup))]

namespace alxbrn_api
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = HttpConfiguration;

            JsonSerializerSettings json = config.Formatters.JsonFormatter.SerializerSettings;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureJwt(app);
            ConfigureWebApi(app, config);
        }

        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            DefaultInlineConstraintResolver constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("identity", typeof(IdConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Filters.Add(new DbUpdateExceptionFilterAttribute());

            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }

        public void ConfigureJwt(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { GlobalConfig.Audience },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(GlobalConfig.Issuer, GlobalConfig.Secret)
                    }
                });
        }
    }
}
