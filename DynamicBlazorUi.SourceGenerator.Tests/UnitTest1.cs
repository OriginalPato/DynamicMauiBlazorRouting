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

namespace TestingModule;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Counter3 : ComponentBase
{
    [Inject] private IRemoteDependencyResolver RemoteDependencyResolver { get; set; }
    [Inject] private SharedCounterService SharedCounterService { get; set; }
    [InjectModuleService]
    private ModuleOnlyService _moduleOnlyService;
    //[InjectModuleService]
    //private ITestService _testService;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _moduleOnlyService = RemoteDependencyResolver.Resolve<ModuleOnlyService>();
        //_testService = RemoteDependencyResolver.Resolve<ITestService, TestService>();
    }

    private void Inc()
    {
        SharedCounterService.IncrementCounter();
    }

    private void Inc2()
    {
        _moduleOnlyService.Increment();
    }

    private void Inc3()
    {
        _testService.DoThing();
    }
}";

        // Pass the source code to our helper and snapshot test the output
        TestHelper.Verify(source);
    }

    private static class TestHelper
    {
        public static void Verify(string source)
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
            driver =
                driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
            GeneratorDriverRunResult runResult = driver.GetRunResult();
            GeneratorRunResult generatorResult = runResult.Results[0];

            var text = generatorResult.GeneratedSources.First().SourceText.ToString();
            text.Should().NotBeEmpty().And.NotBeNull();
            var output = @"// <auto-generated />

using System.Collections.Generic;
using DynamicBlazor.Services;

namespace TestingModule
{
   partial class Counter3
   {
        [Inject] private IRemoteDependencyResolver RemoteDependencyResolver { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _moduleOnlyService = RemoteDependencyResolver.Resolve<ModuleOnlyService>();
        }
   }
}
";
            text.Should().Be(output);
        }
    }
}