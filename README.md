# OPC UA Emulator

A clean, open-source OPC UA emulator for developers building **OPC UA clients, HMIs, SCADA integrations, dashboards, and test environments**.

Built on **.NET** and designed as a **modular monolith**, the project focuses on maintainability, testability, and a straightforward developer experience. Runtime configuration is stored in **PostgreSQL**, while OPC UA **certificates and PKI artifacts should be stored on disk or in a mounted volume**, not in the database.

## Overview

**OPC UA Emulator** helps teams simulate realistic OPC UA servers for local development, integration testing, demos, and internal tooling. It is intended to be easy to run, easy to extend, and structured for long-term evolution without unnecessary complexity.

## Key Features

- Emulates OPC UA server behavior for development and testing workflows
- Built with **ASP.NET Core Web API** and the **OPC Foundation UA .NET Standard** stack
- Stores emulator configuration and related application data in **PostgreSQL**
- Uses **EF Core** and **Npgsql** for persistence
- Container-friendly setup with **Docker Compose**
- Modular monolith architecture with clear boundaries between API, application, domain, and infrastructure
- Includes **unit, integration, and architecture guard / project structure tests**
- Designed for extension as new emulator scenarios and protocols evolve

## Architecture

The solution follows a **modular monolith** approach:

- **API** handles HTTP endpoints and hosting concerns
- **Application** contains use cases, orchestration, and application services
- **Domain** contains core business rules and domain models
- **Contracts** defines shared request/response contracts and DTO-style boundaries
- **Infrastructure** is split by responsibility:
  - OPC UA integration and server-specific behavior
  - persistence and database access

This structure keeps the codebase cohesive while preserving clean boundaries and strong test coverage.

## Solution Structure

```text
OpcUaEmulator.Api
OpcUaEmulator.Application
OpcUaEmulator.Contracts
OpcUaEmulator.Domain
OpcUaEmulator.Infrastructure.OpcUa
OpcUaEmulator.Infrastructure.Persistence
OpcUaEmulator.Application.Tests
OpcUaEmulator.Domain.Tests
OpcUaEmulator.Integration.Tests
```

## Getting Started

### Prerequisites

- .NET SDK
- Docker and Docker Compose
- PostgreSQL (or use the provided containerized setup)

### Run with Docker Compose

1. Clone the repository.
2. Configure environment variables and application settings as needed.
3. Start the required services:

```bash
docker compose up -d
```

4. Apply database migrations if your setup requires it.
5. Start the API project:

```bash
dotnet run --project OpcUaEmulator.Api
```

## PostgreSQL and Storage Notes

- **PostgreSQL** stores emulator configuration and application data.
- **OPC UA certificates, trust lists, and PKI folders should live on disk or in a mounted volume.**
- Avoid storing certificate/private key material in the relational database.

## Testing

Run the full test suite with:

```bash
dotnet test
```

The solution includes:

- **Domain tests** for core business behavior
- **Application tests** for use-case and orchestration logic
- **Integration tests** for cross-boundary behavior
- **Architecture guard / project structure tests** to enforce intended layering and dependencies

## Roadmap

- Expand supported emulator scenarios and node modeling capabilities
- Improve configuration and scenario management APIs
- Add richer developer tooling and observability
- Strengthen test coverage for real-world OPC UA client workflows
- Improve containerized local-development experience

## Contributing

Contributions, issues, and suggestions are welcome.

If you want to contribute:

1. Fork the repository
2. Create a feature branch
3. Add or update tests where appropriate
4. Open a pull request with a clear description of the change

## License

This project is licensed under the **Apache-2.0** license.

You are free to use, modify, and distribute this project in accordance with the terms of the license. See the `LICENSE` file for details.
