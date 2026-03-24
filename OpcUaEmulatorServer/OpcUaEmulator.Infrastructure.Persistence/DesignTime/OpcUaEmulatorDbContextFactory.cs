using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OpcUaEmulator.Infrastructure.Persistence.Db;

namespace OpcUaEmulator.Infrastructure.Persistence.Persistence.DesignTime;

public sealed class OpcUaEmulatorDbContextFactory
    : IDesignTimeDbContextFactory<OpcUaEmulatorDbContext>
{
    public OpcUaEmulatorDbContext CreateDbContext(string[] args)
    {
        var environment =
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Production";

        var apiProjectPath = ResolveApiProjectPath();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new InvalidOperationException(
                "Connection string 'Postgres' was not found.");

        var optionsBuilder = new DbContextOptionsBuilder<OpcUaEmulatorDbContext>();

        optionsBuilder.UseNpgsql(connectionString, npgsql =>
        {
            npgsql.MigrationsAssembly(typeof(OpcUaEmulatorDbContext).Assembly.FullName);
            npgsql.MigrationsHistoryTable(
                "__ef_migrations_history",
                OpcUaEmulatorDbContext.DefaultSchema);
        });

        return new OpcUaEmulatorDbContext(optionsBuilder.Options);
    }

    private static string ResolveApiProjectPath()
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (current is not null)
        {
            var candidate = Path.Combine(current.FullName, "OpcUaEmulator.Api");
            if (Directory.Exists(candidate) &&
                File.Exists(Path.Join(candidate, "appsettings.json")))
            {
                return candidate;
            }

            var candidateFromInfrastructure = Path.Combine(
                current.FullName,
                "Infrastructure",
                "OpcUaEmulator.Api");

            if (Directory.Exists(candidateFromInfrastructure) &&
                File.Exists(Path.Join(candidateFromInfrastructure, "appsettings.json")))
            {
                return candidateFromInfrastructure;
            }

            current = current.Parent;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the OpcUaEmulator.Api project directory.");
    }
}