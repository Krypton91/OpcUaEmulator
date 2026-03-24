using Microsoft.EntityFrameworkCore;
using OpcUaEmulator.Domain.Entities;
using OpcUaEmulator.Infrastructure.Persistence.Db;
using Testcontainers.PostgreSql;
using Xunit;

namespace OpcUaEmulator.Integration.Tests.Persistence;

public sealed class OpcUaEmulatorDbContextTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres =
        new PostgreSqlBuilder("postgres:16.4").Build();

    public async ValueTask InitializeAsync()
    {
        await _postgres.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task CanApplyMigrationsAndPersistEmulatedNode()
    {
        var options = new DbContextOptionsBuilder<OpcUaEmulatorDbContext>()
            .UseNpgsql(_postgres.GetConnectionString(), npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(OpcUaEmulatorDbContext).Assembly.FullName);
            })
            .Options;

        await using var dbContext = new OpcUaEmulatorDbContext(options);

        await dbContext.Database.MigrateAsync(cancellationToken: TestContext.Current.CancellationToken);

        var node = new EmulatedNode(
            Guid.NewGuid(),
            "ns=2;s=Temperature",
            "Temperature",
            "Double",
            "21.5");

        dbContext.EmulatedNodes.Add(node);
        await dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        dbContext.ChangeTracker.Clear();

        var saved = await dbContext.EmulatedNodes
            .SingleAsync(x => x.NodeId == "ns=2;s=Temperature", cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(node.Id, saved.Id);
        Assert.Equal("Temperature", saved.BrowseName);
        Assert.Equal("Double", saved.DataType);
        Assert.Equal("21.5", saved.CurrentValue);
        Assert.True(saved.CreatedAtUtc <= DateTimeOffset.UtcNow);
    }
}