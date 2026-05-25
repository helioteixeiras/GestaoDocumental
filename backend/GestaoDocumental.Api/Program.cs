using System.Security.Claims;
using System.Text;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Api.Mappings;
using Microsoft.EntityFrameworkCore;
using GestaoDocumental.Infrastructure.DependencyInjection;
using GestaoDocumental.Application.DependencyInjection;
using GestaoDocumental.Infrastructure.Security;
using FluentValidation.AspNetCore;
using GestaoDocumental.Api.Middlewares;
using GestaoDocumental.Shared.Security;
using GestaoDocumental.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(JwtSettings.SectionName));

var jwtSettings = builder.Configuration
    .GetSection(JwtSettings.SectionName)
    .Get<JwtSettings>() ?? throw new InvalidOperationException("JwtSettings configuration is missing.");

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AppPolicies.AdministradorOnly, policy =>
        policy.RequireRole(AppRoles.Administrador));

    options.AddPolicy(AppPolicies.PodeGerirUsuarios, policy =>
        policy.RequireRole(AppRoles.Administrador));

    options.AddPolicy(AppPolicies.PodeGerirDocumentos, policy =>
        policy.RequireRole(AppRoles.Administrador, AppRoles.Operador));

    options.AddPolicy(AppPolicies.PodeConsultarDocumentos, policy =>
        policy.RequireRole(AppRoles.Administrador, AppRoles.Operador, AppRoles.Consulta));

    options.AddPolicy(AppPolicies.PodeAprovarDocumentos, policy =>
        policy.RequireRole(AppRoles.Administrador, AppRoles.Operador));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<GestaoDocumentalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
builder.Services.AddApplication();


var app = builder.Build();

app.UseGlobalExceptionMiddleware();

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<AutoMapper.IMapper>();

    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

await IdentityDataSeeder.SeedAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
