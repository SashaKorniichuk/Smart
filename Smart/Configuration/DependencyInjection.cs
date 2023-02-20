using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Scrutor;

namespace Smart.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Persistence.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Application.AssemblyReference.Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddValidatorsFromAssembly(
                Application.AssemblyReference.Assembly,
                includeInternalTypes: true);

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                dbContextOptionBuilder =>
                {
                    var connectionString = configuration.GetConnectionString("Database");

                    dbContextOptionBuilder.UseSqlServer(connectionString);
                });

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
                 .AddControllers()
                 .AddApplicationPart(Presentation.AssemblyReference.Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart", Version = "v1" });

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Description = "The API Key to access the API",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "x-api-key",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header,
                };
                var requirement = new OpenApiSecurityRequirement
                {
                    {scheme,new List<string>()}
                };
                c.AddSecurityRequirement(requirement);
            });

            return services;
        }
    }
}
