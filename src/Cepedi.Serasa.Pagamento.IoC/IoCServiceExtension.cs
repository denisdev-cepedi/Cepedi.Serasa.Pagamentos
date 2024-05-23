using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dados.Repositories;
using Cepedi.Serasa.Pagamento.Dados.Repositorios.Queries;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Pipelines;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using Cepedi.Serasa.Pagamento.Dominio.Servicos;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;

namespace Cepedi.Serasa.Pagamento.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoCServiceExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExcecaoPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidacaoComportamento<,>));
            ConfigurarFluentValidation(services);
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ICredorRepository, CredorRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IDividaRepository, DividaRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPessoaQueryRepository, PessoaQueryRepository>();

            // Cache Redis
            services.AddStackExchangeRedisCache(obj =>
            {
                obj.Configuration = configuration["Redis::Connection"];
                obj.InstanceName = configuration["Redis::Instance"];
            });

            services.AddSingleton<IDistributedCache, RedisCache>();
            services.AddScoped(typeof(ICache<>), typeof(Cache<>));

            ConfigurarSso(services, configuration);

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
        }

        private static void ConfigurarFluentValidation(IServiceCollection services)
        {
            var abstractValidator = typeof(AbstractValidator<>);
            var validadores = typeof(QualquerCoisa)
                .Assembly
                .DefinedTypes
                .Where(type => type.BaseType?.IsGenericType is true &&
                type.BaseType.GetGenericTypeDefinition() ==
                abstractValidator)
                .Select(Activator.CreateInstance)
                .ToArray();

            foreach (var validator in validadores)
            {
                services.AddSingleton(validator!.GetType().BaseType!, validator);
            }
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }

        private static void ConfigurarSso(IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

            services.AddKeycloakAuthentication(authenticationOptions!);


            var authorizationOptions = configuration
                                        .GetSection(KeycloakProtectionClientOptions.Section)
                                        .Get<KeycloakProtectionClientOptions>();

            services.AddKeycloakAuthorization(authorizationOptions);

            var adminClientOptions = configuration
                                        .GetSection(KeycloakAdminClientOptions.Section)
                                        .Get<KeycloakAdminClientOptions>();

            services.AddKeycloakAdminHttpClient(adminClientOptions);
        }
    }
}
