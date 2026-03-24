using NetArchTest.Rules;
using Xunit;

namespace OpcUaEmulator.Integration.Tests
{
    public sealed class ArchitectureTests
    {
        [Fact]
        public void DomainShouldNotDependOnApplication()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Application")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainShouldNotDependOnApi()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainShouldNotDependOnInfrastructurePersistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainShouldNotDependOnInfrastructureOpcUa()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApplicationShouldNotDependOnApi()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApplicationShouldNotDependOnInfrastructurePersistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApplicationShouldNotDependOnInfrastructureOpcUa()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ContractsShouldNotDependOnDomain()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Domain")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ContractsShouldNotDependOnApplication()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Application")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ContractsShouldNotDependOnInfrastructure()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void PersistenceShouldNotDependOnApi()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.Persistence.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void PersistenceShouldNotDependOnOpcUaInfrastructure()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.Persistence.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void OpcUaInfrastructureShouldNotDependOnApi()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.OpcUa.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void OpcUaInfrastructureShouldNotDependOnPersistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.OpcUa.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApiShouldNotDependOnDomainDirectly()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Api.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Domain")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
