using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BlazingComponents.Authentication.Controllers;
using Microsoft.AspNetCore.Authentication;
using BlazingComponents.Authentication.Handlers;

namespace BlazingComponents.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void UseBlazingAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        public static void AddBlazingAuthentication(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null)
                .AddCookie();

            services.AddAuthorization();
            mvcBuilder
                .AddApplicationPart(typeof(UserController).GetTypeInfo().Assembly)
                .AddControllersAsServices();
        }
    }
}
