using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Repositories;
using GestaoDocumental.Infrastructure.Security;
using GestaoDocumental.Infrastructure.Export;
using GestaoDocumental.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDocumental.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUsuarioSistemaRepository, UsuarioSistemaRepository>();
        services.AddScoped<IDocumentoAnexoRepository, DocumentoAnexoRepository>();
        services.AddScoped<IDocumentoWorkflowRepository, DocumentoWorkflowRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();
        services.AddScoped<ICsvExportService, CsvExportService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}