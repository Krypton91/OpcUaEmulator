using System.Xml.Linq;
using Xunit;

namespace OpcUaEmulator.Integration.Tests;

public sealed class ProjectStructureTests
{
    [Fact]
    public void Solution_Should_Have_Expected_Project_References()
    {
        var root = FindRepositoryRoot();

        var expected = new Dictionary<string, string[]>
        {
            ["OpcUaEmulator.Domain"] = Array.Empty<string>(),
            ["OpcUaEmulator.Contracts"] = Array.Empty<string>(),

            ["OpcUaEmulator.Application"] = new[]
            {
                "OpcUaEmulator.Domain"
            },

            ["OpcUaEmulator.Infrastructure.Persistence"] = new[]
            {
                "OpcUaEmulator.Application",
                "OpcUaEmulator.Domain"
            },

            ["OpcUaEmulator.Infrastructure.OpcUa"] = new[]
            {
                "OpcUaEmulator.Application",
                "OpcUaEmulator.Domain"
            },

            ["OpcUaEmulator.Api"] = new[]
            {
                "OpcUaEmulator.Application",
                "OpcUaEmulator.Contracts",
                "OpcUaEmulator.Infrastructure.OpcUa",
                "OpcUaEmulator.Infrastructure.Persistence"
            },

            ["OpcUaEmulator.Domain.Tests"] = new[]
            {
                "OpcUaEmulator.Domain"
            },

            ["OpcUaEmulator.Application.Tests"] = new[]
            {
                "OpcUaEmulator.Application",
                "OpcUaEmulator.Domain"
            },

            ["OpcUaEmulator.Integration.Tests"] = new[]
            {
                "OpcUaEmulator.Api",
                "OpcUaEmulator.Application",
                "OpcUaEmulator.Contracts",
                "OpcUaEmulator.Domain",
                "OpcUaEmulator.Infrastructure.OpcUa",
                "OpcUaEmulator.Infrastructure.Persistence"
            }
        };

        foreach (var project in expected)
        {
            var csprojPath = Path.Combine(root, project.Key, $"{project.Key}.csproj");
            Assert.True(File.Exists(csprojPath), $"Project file not found: {csprojPath}");

            var actualReferences = ReadProjectReferences(csprojPath);

            var expectedSorted = project.Value.OrderBy(x => x).ToArray();
            var actualSorted = actualReferences.OrderBy(x => x).ToArray();

            Assert.Equal(expectedSorted, actualSorted);
        }
    }

    [Fact]
    public void Solution_Should_Contain_All_Expected_Project_Folders()
    {
        var root = FindRepositoryRoot();

        var expectedFolders = new[]
        {
            "OpcUaEmulator.Api",
            "OpcUaEmulator.Application",
            "OpcUaEmulator.Contracts",
            "OpcUaEmulator.Domain",
            "OpcUaEmulator.Infrastructure.OpcUa",
            "OpcUaEmulator.Infrastructure.Persistence",
            "OpcUaEmulator.Application.Tests",
            "OpcUaEmulator.Domain.Tests",
            "OpcUaEmulator.Integration.Tests"
        };

        foreach (var folder in expectedFolders)
        {
            var fullPath = Path.Combine(root, folder);
            Assert.True(Directory.Exists(fullPath), $"Expected folder missing: {fullPath}");
        }
    }

    private static string[] ReadProjectReferences(string csprojPath)
    {
        var document = XDocument.Load(csprojPath);

        return document.Descendants()
            .Where(x => x.Name.LocalName == "ProjectReference")
            .Select(x => x.Attribute("Include")?.Value)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(GetProjectNameFromReference)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray()!;
    }

    private static string GetProjectNameFromReference(string referencePath)
    {
        var normalized = referencePath.Replace('\\', Path.DirectorySeparatorChar)
                                      .Replace('/', Path.DirectorySeparatorChar);

        var fileName = Path.GetFileNameWithoutExtension(normalized);
        return fileName;
    }

    private static string FindRepositoryRoot()
    {
        var current = AppContext.BaseDirectory;

        while (!string.IsNullOrWhiteSpace(current))
        {
            var slnxFiles = Directory.GetFiles(current, "*.slnx");
            var slnFiles = Directory.GetFiles(current, "*.sln");

            if (slnxFiles.Length > 0 || slnFiles.Length > 0)
                return current;

            var parent = Directory.GetParent(current);
            if (parent is null)
                break;

            current = parent.FullName;
        }

        throw new InvalidOperationException("Repository root with .sln or .slnx not found.");
    }
}