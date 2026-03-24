# Database and testing guide

This project uses **PostgreSQL** together with **Entity Framework Core**.

The persistence implementation is located in `OpcUaEmulator.Infrastructure.Persistence`.
The API project is used as the startup project for EF Core tooling because the connection string is loaded from the API configuration.

## Where database-related code lives

- `OpcUaEmulator.Domain`
  - Entities and domain logic only
  - No EF Core attributes or infrastructure dependencies
- `OpcUaEmulator.Application`
  - Interfaces and application use cases
  - No dependency on `OpcUaEmulator.Infrastructure.Persistence`
- `OpcUaEmulator.Infrastructure.Persistence`
  - `DbContext`
  - entity configurations
  - repositories / unit of work
  - migrations
  - design-time factory for `dotnet ef`
- `OpcUaEmulator.Api`
  - startup project
  - `appsettings.json` and `appsettings.Development.json`
  - dependency injection wiring

This separation is important because architecture tests enforce these boundaries.

## Configuration

The database connection string is read from:

- `OpcUaEmulator.Api/appsettings.json`
- `OpcUaEmulator.Api/appsettings.Development.json`

Example:

```json
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=opcua_emulator;Username=postgres;Password=postgres"
  }
}
```

For local development, make sure:

- PostgreSQL is running
- the `Postgres` connection string is valid
- the environment is set to `Development` when using local development settings

## Apply migrations

To apply the latest migrations to your local database:

```powershell
$Env:ASPNETCORE_ENVIRONMENT = "Development"; dotnet ef database update --project .\Infrastructure\OpcUaEmulator.Infrastructure.Persistence\OpcUaEmulator.Infrastructure.Persistence.csproj --startup-project .\OpcUaEmulator.Api\OpcUaEmulator.Api.csproj --context OpcUaEmulatorDbContext
```

## Create a new migration

Whenever you change the EF Core model or entity mappings, create a new migration:

```powershell
$Env:ASPNETCORE_ENVIRONMENT = "Development"; dotnet ef migrations add <MigrationName> --project .\Infrastructure\OpcUaEmulator.Infrastructure.Persistence\OpcUaEmulator.Infrastructure.Persistence.csproj --startup-project .\OpcUaEmulator.Api\OpcUaEmulator.Api.csproj --context OpcUaEmulatorDbContext --output-dir Persistence\Migrations
```

Example:

```powershell
$Env:ASPNETCORE_ENVIRONMENT = "Development"; dotnet ef migrations add AddEmulatedNodeHistory --project .\Infrastructure\OpcUaEmulator.Infrastructure.Persistence\OpcUaEmulator.Infrastructure.Persistence.csproj --startup-project .\OpcUaEmulator.Api\OpcUaEmulator.Api.csproj --context OpcUaEmulatorDbContext --output-dir Persistence\Migrations
```

## Recommended database workflow

1. Change the domain model and persistence configuration.
2. Create a migration.
3. Review the generated migration files.
4. Apply the migration locally.
5. Run the full test suite.
6. Commit the code changes and migration files together.

## Running tests

### Run all tests

```powershell
dotnet test
```

### Run only integration tests

```powershell
dotnet test .\OpcUaEmulator.Integration.Tests\OpcUaEmulator.Integration.Tests.csproj
```

### Run only application tests

```powershell
dotnet test .\OpcUaEmulator.Application.Tests\OpcUaEmulator.Application.Tests.csproj
```

### Run only domain tests

```powershell
dotnet test .\OpcUaEmulator.Domain.Tests\OpcUaEmulator.Domain.Tests.csproj
```

## Integration tests and PostgreSQL

Persistence-related tests should prefer a real PostgreSQL database instead of an in-memory provider when the goal is to validate database behavior, mappings, migrations, uniqueness constraints, or provider-specific SQL behavior.

If integration tests use **Testcontainers**:

- Docker must be running locally
- CI can run the same tests on GitHub-hosted Ubuntu runners
- no separate PostgreSQL service is required if the tests start their own container

## What to verify before opening a pull request

Before creating a PR, make sure that:

- the solution builds successfully
- migrations were created intentionally and reviewed
- the database can be updated locally
- all tests pass
- architecture tests still pass

## Common issues

### `dotnet ef` cannot find the connection string

Check that:

- `ASPNETCORE_ENVIRONMENT=Development` is set when needed
- `appsettings.Development.json` exists in `OpcUaEmulator.Api`
- the design-time factory loads configuration from the API project

### Integration tests fail because Docker is not available

Start Docker Desktop or your local Docker daemon before running integration tests that use Testcontainers.
