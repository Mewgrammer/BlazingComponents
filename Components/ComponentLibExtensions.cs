using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Toaster.Core.Models;

namespace BlazorEssentials.ComponentLib
{
    public static class ComponentLibExtensions
    {

        public static void UseComponentLib(this IApplicationBuilder app)
        {

        }

        public static void AddComponentLib(this IServiceCollection services)
        {
            services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
            });
        }
}
}
