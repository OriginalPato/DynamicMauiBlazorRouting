using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DynamicBlazorUi.SourceGenerator.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IncrementalSourceGeneratorTest()
    {
        var source = @"
using DemoLibrary;

namespace TestingSourceGenerator;

[EnumGeneration]
public partial class ProductCategory
{
    public static readonly ProductCategory Fruits = new(""Fruits"");
    public static readonly ProductCategory Dairy = new(""Dairy"");

    public string Name { get; }

    private ProductCategory(string name)
    {
        Name = name;
    }
}";

        // Pass the source code to our helper and snapshot test the output
        TestHelper.Verify(source);
    }

    private static class TestHelper
    {
        public static Task Verify(string source)
        {
            // Parse the provided string into a C# syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

            // Create a Roslyn compilation for the syntax tree.
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "Tests",
                syntaxTrees: new[] {syntaxTree});


            // Create an instance of our EnumGenerator incremental source generator
            var generator = new DemoSourceGenerator();

            // The GeneratorDriver is used to run our generator against a compilation
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            // Run the source generator!
            driver = driver.RunGenerators(compilation);
            var driver2 =
                driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
            GeneratorDriverRunResult runResult = driver2.GetRunResult();
            GeneratorRunResult generatorResult = runResult.Results[0];

            var text = generatorResult.GeneratedSources.First().SourceText.ToString();
            text.Should().NotBeEmpty().And.NotBeNull();
            // Use verify to snapshot test the source generator output!
            return Task.CompletedTask;
        }
    }
}