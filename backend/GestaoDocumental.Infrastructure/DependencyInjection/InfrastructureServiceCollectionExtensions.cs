using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Repositories;
using GestaoDocumental.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDocumental.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUsuarioSistemaRepository, UsuarioSistemaRepository>();
        services.AddScoped<IDocumentoWorkflowRepository, DocumentoWorkflowRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}