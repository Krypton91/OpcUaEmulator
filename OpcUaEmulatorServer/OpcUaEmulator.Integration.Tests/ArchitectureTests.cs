using NetArchTest.Rules;
using Xunit;

namespace OpcUaEmulator.Integration.Tests
{
    public sealed class ArchitectureTests
    {
        [Fact]
        public void Domain_Should_Not_Depend_On_Application()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Application")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Domain_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Domain_Should_Not_Depend_On_Infrastructure_Persistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Domain_Should_Not_Depend_On_Infrastructure_OpcUa()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Domain.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Persistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_OpcUa()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Application.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Contracts_Should_Not_Depend_On_Domain()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Domain")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Contracts_Should_Not_Depend_On_Application()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Application")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Contracts_Should_Not_Depend_On_Infrastructure()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Contracts.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Persistence_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.Persistence.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Persistence_Should_Not_Depend_On_OpcUa_Infrastructure()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.Persistence.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.OpcUa")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void OpcUa_Infrastructure_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.OpcUa.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Api")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void OpcUa_Infrastructure_Should_Not_Depend_On_Persistence()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Infrastructure.OpcUa.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Infrastructure.Persistence")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Api_Should_Not_Depend_On_Domain_Directly()
        {
            var result = Types.InAssembly(typeof(OpcUaEmulator.Api.AssemblyMarker).Assembly)
                .ShouldNot()
                .HaveDependencyOn("OpcUaEmulator.Domain")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
