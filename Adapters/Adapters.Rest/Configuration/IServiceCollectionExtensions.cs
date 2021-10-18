using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Adapters.Rest.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRestAdapterServices(this IServiceCollection services) 
        {
            services
                .AddControllers();
            
            return services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
                });
        }
    }
}