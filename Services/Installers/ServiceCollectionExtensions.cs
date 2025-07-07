using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.Infrastructure;

namespace Services.Installers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallServices(this IServiceCollection services)
        {
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            return services;
        }
    }
}
