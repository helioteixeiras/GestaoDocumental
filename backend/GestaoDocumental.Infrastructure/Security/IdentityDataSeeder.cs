using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GestaoDocumental.Infrastructure.Security;

public static class IdentityDataSeeder
{
    private const string AdminUsername = "admin";
    private const string AdminPassword = "Admin123*";
    private const string AdminEmail = "admin@gestaodocumental.local";
    private const string AdminPerfilName = "Administrador";
    private const string ActiveLoginStateName = "Ativo";

    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<GestaoDocumentalDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("IdentityDataSeeder");

        if (await context.UsuarioSistemas.AnyAsync(usuario => usuario.Username == AdminUsername))
        {
            logger.LogInformation("Admin user already exists. Seed skipped.");
            return;
        }

        var perfil = await context.Perfils
            .FirstOrDefaultAsync(item => item.Nome == AdminPerfilName);

        if (perfil is null)
        {
            perfil = new Perfil
            {
                Nome = AdminPerfilName,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            context.Perfils.Add(perfil);
            await context.SaveChangesAsync();
        }

        var estadoLogin = await context.EstadoLogins
            .FirstOrDefaultAsync(item => item.Nome == ActiveLoginStateName);

        if (estadoLogin is null)
        {
            estadoLogin = new EstadoLogin
            {
                Nome = ActiveLoginStateName,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            context.EstadoLogins.Add(estadoLogin);
            await context.SaveChangesAsync();
        }

        var adminUser = new UsuarioSistema
        {
            Username = AdminUsername,
            Email = AdminEmail,
            PasswordHash = passwordHasher.HashPassword(AdminPassword),
            PerfilId = perfil.Id,
            EstadoLoginId = estadoLogin.Id,
            TentativasLogin = 0,
            Bloqueado = false,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        context.UsuarioSistemas.Add(adminUser);
        await context.SaveChangesAsync();

        logger.LogInformation("Admin user seeded successfully.");
    }
}
