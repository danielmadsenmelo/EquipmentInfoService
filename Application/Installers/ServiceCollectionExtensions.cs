using Application.UseCases.GetEquipmentStatusUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Installers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services)
        {
            services.AddScoped<IGetEquipmentStatusUseCase, GetEquipmentStatusUseCase>();

            return services;
        }
    }
}
