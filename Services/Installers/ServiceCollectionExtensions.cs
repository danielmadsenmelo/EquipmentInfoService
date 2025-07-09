using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.Infrastructure;

namespace Services.Installers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallServices(this IServiceCollection services)
        {
            services.AddOptions<EquipmentDatabaseSettings>();
            services.AddSingleton<IEquipmentRepository, EquipmentRepository>();

            return services;
        }
    }
}
