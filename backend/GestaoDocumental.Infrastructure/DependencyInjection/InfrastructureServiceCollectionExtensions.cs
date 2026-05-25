using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDocumental.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}