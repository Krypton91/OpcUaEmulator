using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpcUaEmulator.Application.Common.Interfaces;
using OpcUaEmulator.Infrastructure.Persistence.Db;
using OpcUaEmulator.Infrastructure.Persistence.Repositories;
using OpcUaEmulator.Infrastructure.Persistence.UnitOfWork;

namespace OpcUaEmulator.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Connection string 'Postgres' was not found.");

            services.AddDbContext<OpcUaEmulatorDbContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(OpcUaEmulatorDbContext).Assembly.FullName);
                });
            });

            services.AddScoped<IEmulatedNodeRepository, EmulatedNodeRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
