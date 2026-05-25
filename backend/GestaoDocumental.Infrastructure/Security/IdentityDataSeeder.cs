using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Shared.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GestaoDocumental.Infrastructure.Security;

public static class IdentityDataSeeder
{
    private const string ActiveLoginStateName = "Ativo";
    private const string TestDocumentNumber = "DOC-TEST-AUTH-001";

    private static readonly (string Username, string Password, string Email, string PerfilName)[] TestUsers =
    [
        ("admin", "Admin123*", "admin@gestaodocumental.local", AppRoles.Administrador),
        ("operador", "Operador123*", "operador@gestaodocumental.local", AppRoles.Operador),
        ("consulta", "Consulta123*", "consulta@gestaodocumental.local", AppRoles.Consulta)
    ];

    public static Task SeedAsync(IServiceProvider serviceProvider) =>
        SeedIdentityAsync(serviceProvider);

    public static Task SeedAdminAsync(IServiceProvider serviceProvider) =>
        SeedIdentityAsync(serviceProvider);

    public static async Task SeedIdentityAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<GestaoDocumentalDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("IdentityDataSeeder");

        var estadoLogin = await EnsureEstadoLoginAsync(context, logger);
        await EnsurePerfisAsync(context, logger);
        await EnsureColaboradorDependenciesAsync(context, logger);
        await EnsureTestUsersAsync(context, passwordHasher, estadoLogin.Id, logger);
        await EnsureDocumentTestFixturesAsync(context, logger);
    }

    private static async Task<EstadoLogin> EnsureEstadoLoginAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        var estadoLogin = await context.EstadoLogins
            .FirstOrDefaultAsync(item => item.Nome == ActiveLoginStateName);

        if (estadoLogin is not null)
            return estadoLogin;

        estadoLogin = new EstadoLogin
        {
            Nome = ActiveLoginStateName,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        context.EstadoLogins.Add(estadoLogin);
        await context.SaveChangesAsync();

        logger.LogInformation("EstadoLogin '{Nome}' seeded.", ActiveLoginStateName);
        return estadoLogin;
    }

    private static async Task EnsurePerfisAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        var perfilNames = new[]
        {
            AppRoles.Administrador,
            AppRoles.Operador,
            AppRoles.Consulta
        };

        foreach (var perfilName in perfilNames)
        {
            if (await context.Perfils.AnyAsync(item => item.Nome == perfilName))
                continue;

            context.Perfils.Add(new Perfil
            {
                Nome = perfilName,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            });

            logger.LogInformation("Perfil '{Nome}' seeded.", perfilName);
        }

        await context.SaveChangesAsync();
    }

    private static async Task EnsureTestUsersAsync(
        GestaoDocumentalDbContext context,
        IPasswordHasher passwordHasher,
        int estadoLoginId,
        ILogger logger)
    {
        foreach (var (username, password, email, perfilName) in TestUsers)
        {
            if (await context.UsuarioSistemas.AnyAsync(usuario => usuario.Username == username))
            {
                logger.LogInformation("User '{Username}' already exists. Seed skipped.", username);
                continue;
            }

            var perfil = await context.Perfils
                .FirstAsync(item => item.Nome == perfilName);

            int? colaboradorId = username == "admin"
                ? null
                : await EnsureUserColaboradorIdAsync(context, username, perfilName, logger);

            context.UsuarioSistemas.Add(new UsuarioSistema
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHasher.HashPassword(password),
                PerfilId = perfil.Id,
                ColaboradorId = colaboradorId,
                EstadoLoginId = estadoLoginId,
                TentativasLogin = 0,
                Bloqueado = false,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
            logger.LogInformation("User '{Username}' seeded successfully.", username);
        }
    }

    private static async Task<int> EnsureUserColaboradorIdAsync(
        GestaoDocumentalDbContext context,
        string username,
        string perfilName,
        ILogger logger)
    {
        var (nome, numDocumento) = username switch
        {
            "operador" => ("Colaborador Operador Auth", "DOC-COL-OPER-001"),
            "consulta" => ("Colaborador Consulta Auth", "DOC-COL-CONS-001"),
            _ => ($"Colaborador {username} Auth", $"DOC-COL-{username.ToUpperInvariant()}-001")
        };

        var perfilColaborador = await context.Perfils.FirstAsync(item => item.Nome == perfilName);
        var postoTrabalho = await EnsurePostoTrabalhoAsync(context, logger);
        var tipoDocumentoColaborador = await context.TipoDocumentoColaboradors
            .FirstAsync(item => item.Nome == "Tipo Doc Colaborador Teste");
        var estadoColaborador = await context.EstadoColaboradors
            .FirstAsync(item => item.Nome == "Ativo Colaborador Teste");

        var colaborador = await EnsureReferenceAsync(
            context,
            context.Colaboradors,
            item => item.NumDocumento == numDocumento,
            () => new Colaborador
            {
                Nome = nome,
                NumDocumento = numDocumento,
                PostoTrabalhoId = postoTrabalho.Id,
                TipoDocumentoColaboradorId = tipoDocumentoColaborador.Id,
                EstadoId = estadoColaborador.Id,
                PerfilId = perfilColaborador.Id,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            logger,
            nome);

        return colaborador.Id;
    }

    private static async Task EnsureColaboradorDependenciesAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        await EnsureReferenceAsync(
            context,
            context.TipoDocumentos,
            item => item.Nome == "Tipo Teste Auth",
            () => new TipoDocumento { Nome = "Tipo Teste Auth", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "TipoDocumento");

        await EnsureReferenceAsync(
            context,
            context.ClassificacaoDocumentos,
            item => item.Nome == "Classificacao Teste Auth",
            () => new ClassificacaoDocumento { Nome = "Classificacao Teste Auth", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "ClassificacaoDocumento");

        await EnsureReferenceAsync(
            context,
            context.EstadoDocumentos,
            item => item.Nome == "Estado Teste Auth",
            () => new EstadoDocumento { Nome = "Estado Teste Auth", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "EstadoDocumento");

        await EnsureReferenceAsync(
            context,
            context.Direcaos,
            item => item.Nome == "Direcao Teste Auth",
            () => new Direcao { Nome = "Direcao Teste Auth", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "Direcao");

        await EnsureReferenceAsync(
            context,
            context.EstadoColaboradors,
            item => item.Nome == "Ativo Colaborador Teste",
            () => new EstadoColaborador { Nome = "Ativo Colaborador Teste", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "EstadoColaborador");

        await EnsureReferenceAsync(
            context,
            context.TipoDocumentoColaboradors,
            item => item.Nome == "Tipo Doc Colaborador Teste",
            () => new TipoDocumentoColaborador { Nome = "Tipo Doc Colaborador Teste", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "TipoDocumentoColaborador");

        await EnsurePostoTrabalhoAsync(context, logger);
    }

    private static async Task EnsureDocumentTestFixturesAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        if (await context.Documentos.AnyAsync(item => item.NumeroDocumento == TestDocumentNumber))
            return;

        var tipoDocumento = await context.TipoDocumentos
            .FirstAsync(item => item.Nome == "Tipo Teste Auth");
        var classificacao = await context.ClassificacaoDocumentos
            .FirstAsync(item => item.Nome == "Classificacao Teste Auth");
        var estadoDocumento = await context.EstadoDocumentos
            .FirstAsync(item => item.Nome == "Estado Teste Auth");
        var direcao = await context.Direcaos
            .FirstAsync(item => item.Nome == "Direcao Teste Auth");
        var colaborador = await context.Colaboradors
            .FirstAsync(item => item.Nome == "Colaborador Operador Auth");

        context.Documentos.Add(new Documento
        {
            NumeroDocumento = TestDocumentNumber,
            Titulo = "Documento Teste Auth",
            TipoDocumentoId = tipoDocumento.Id,
            ClassificacaoId = classificacao.Id,
            EstadoDocumentoId = estadoDocumento.Id,
            DirecaoOrigemId = direcao.Id,
            ColaboradorCriadorId = colaborador.Id,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        });

        await context.SaveChangesAsync();
        logger.LogInformation("Document test fixtures seeded.");
    }

    private static async Task<PostoTrabalho> EnsurePostoTrabalhoAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        var existing = await context.PostoTrabalhos
            .FirstOrDefaultAsync(item => item.Nome == "Posto Teste Auth");

        if (existing is not null)
            return existing;

        var direcao = await context.Direcaos.FirstAsync(item => item.Nome == "Direcao Teste Auth");

        var departamento = await EnsureReferenceAsync(
            context,
            context.Departamentos,
            item => item.Nome == "Departamento Teste Auth",
            () => new Departamento
            {
                Nome = "Departamento Teste Auth",
                DirecaoId = direcao.Id,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            logger,
            "Departamento");

        var municipio = await EnsureMunicipioAsync(context, logger);

        existing = new PostoTrabalho
        {
            Nome = "Posto Teste Auth",
            DepartamentoId = departamento.Id,
            MunicipioId = municipio.Id,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        context.PostoTrabalhos.Add(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("PostoTrabalho test fixture seeded.");

        return existing;
    }

    private static async Task<Municipio> EnsureMunicipioAsync(
        GestaoDocumentalDbContext context,
        ILogger logger)
    {
        var existing = await context.Municipios
            .FirstOrDefaultAsync(item => item.Nome == "Municipio Teste Auth");

        if (existing is not null)
            return existing;

        var pais = await EnsureReferenceAsync(
            context,
            context.Pais,
            item => item.Nome == "Pais Teste Auth",
            () => new Pais { Nome = "Pais Teste Auth", Ativo = true, DataCriacao = DateTime.UtcNow },
            logger,
            "Pais");

        var provincia = await EnsureReferenceAsync(
            context,
            context.Provincia,
            item => item.Nome == "Provincia Teste Auth",
            () => new Provincia
            {
                Nome = "Provincia Teste Auth",
                PaisId = pais.Id,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            logger,
            "Provincia");

        existing = new Municipio
        {
            Nome = "Municipio Teste Auth",
            ProvinciaId = provincia.Id,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        context.Municipios.Add(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("Municipio test fixture seeded.");

        return existing;
    }

    private static async Task<TEntity> EnsureReferenceAsync<TEntity>(
        GestaoDocumentalDbContext context,
        DbSet<TEntity> dbSet,
        System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
        Func<TEntity> factory,
        ILogger logger,
        string entityName)
        where TEntity : class
    {
        var existing = await dbSet.FirstOrDefaultAsync(predicate);

        if (existing is not null)
            return existing;

        existing = factory();
        dbSet.Add(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("{EntityName} test fixture seeded.", entityName);

        return existing;
    }
}
