using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Interfaces.Services;
using Inventory.Application.Services;
namespace Inventory.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
