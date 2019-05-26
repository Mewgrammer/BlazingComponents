using BlazorEssentials.Authentication.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorEssentials.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void UseBlazorEssentialsAuthWithMvc(this IApplicationBuilder app)
        {
            app.UseMvc();
        }
        public static void AddBlazorEssentialsAuthWithMvc(this IServiceCollection services)
        {
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddApplicationPart(typeof(UserController).GetTypeInfo().Assembly)
            .AddControllersAsServices();
        }
    }
}
