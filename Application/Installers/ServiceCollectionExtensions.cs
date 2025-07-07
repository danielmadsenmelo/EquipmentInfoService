using Application.UseCases.GetEquipmentStatusUseCase;
using Application.UseCases.UpsertEquipmentStatusUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Installers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services)
        {
            services.AddScoped<IGetEquipmentStatusUseCase, GetEquipmentStatusUseCase>();
            services.AddScoped<IUpsertEquipmentStatusUseCase, UpsertEquipmentStatusUseCase>();

            return services;
        }
    }
}
