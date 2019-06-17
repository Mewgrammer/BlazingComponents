using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BlazingComponents.Authentication.Controllers;

namespace BlazingComponents.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void UseBlazingComponentsAuthWithMvc(this IApplicationBuilder app)
        {
            app.UseMvc();
        }
        public static void AddBlazingComponentsAuthWithMvc(this IServiceCollection services)
        {
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddApplicationPart(typeof(UserController).GetTypeInfo().Assembly)
            .AddControllersAsServices();
        }
    }
}
