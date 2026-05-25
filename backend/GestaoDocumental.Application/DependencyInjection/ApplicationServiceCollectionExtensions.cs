using FluentValidation;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Application.Services;
using GestaoDocumental.Application.Services.Auth;
using GestaoDocumental.Application.Validators.ClassificacaoDocumento;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDocumental.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ClassificacaoDocumentoCreateDtoValidator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClassificacaoDocumentoService, ClassificacaoDocumentoService>();
        services.AddScoped<IColaboradorService, ColaboradorService>();
        services.AddScoped<IUsuarioSistemaService, UsuarioSistemaService>();
        services.AddScoped<IDepartamentoService, DepartamentoService>();
        services.AddScoped<IDirecaoService, DirecaoService>();
        services.AddScoped<IPostoTrabalhoService, PostoTrabalhoService>();
        services.AddScoped<IGeneroService, GeneroService>();
        services.AddScoped<IPerfilService, PerfilService>();
        services.AddScoped<IPaisService, PaisService>();
        services.AddScoped<IProvinciaService, ProvinciaService>();
        services.AddScoped<IMunicipioService, MunicipioService>();
        services.AddScoped<IEstadoDocumentoService, EstadoDocumentoService>();
        services.AddScoped<IEstadoColaboradorService, EstadoColaboradorService>();
        services.AddScoped<IEstadoLoginService, EstadoLoginService>();
        services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
        services.AddScoped<ITipoDocumentoColaboradorService, TipoDocumentoColaboradorService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IDocumentoHistoricoService, DocumentoHistoricoService>();
        services.AddScoped<IDocumentoAnexoService, DocumentoAnexoService>();
        services.AddScoped<IDocumentoService, DocumentoService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ITramitacaoDocumentoService, TramitacaoDocumentoService>();

        return services;
    }
}
