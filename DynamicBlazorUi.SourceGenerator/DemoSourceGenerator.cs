﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DynamicBlazorUi.SourceGenerator;

[Generator]
public class DemoSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(CouldBeEnumerationAsync, GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect();

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<MyRet> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
            return;
        var code = GenerateCode(enumerations);

        context.AddSource($"{enumerations.First().Namespace}{enumerations.First().Name}.g.cs", code);
    }

    private static string GenerateCode(ImmutableArray<MyRet> type)
    {
        var ret = $@"// <auto-generated />

using System.Collections.Generic;
using DynamicBlazor.Services;
using TestingModule.Services;
using Microsoft.AspNetCore.Components;

namespace {type.First().Namespace}
{{
   partial class {type.First().ClassName}
   {{
        [Inject] private IRemoteDependencyResolver RemoteDependencyResolver {{ get; set; }}

        protected async Task RegisterRemoteServices()
        {{
";
        ret = Enumerable.Aggregate(type, ret, (current, myRet) => current + $@"{myRet.Name} = RemoteDependencyResolver.Resolve<{myRet.ServiceName}>();
");
        ret += $@"
        }}
   }}
}}
";
        return ret;
    }

    private static bool CouldBeEnumerationAsync(
        SyntaxNode syntaxNode,
        CancellationToken cancellationToken)
    {
        if (syntaxNode is not AttributeSyntax attribute)
            return false;

        var name = ExtractName(attribute.Name);

        return name is "InjectModuleService" or "InjectModuleServiceAttribute";
    }

    private static string? ExtractName(NameSyntax? name)
    {
        return name switch
        {
            SimpleNameSyntax ins => ins.Identifier.Text,
            QualifiedNameSyntax qns => qns.Right.Identifier.Text,
            _ => null
        };
    }

    private static MyRet? GetEnumTypeOrNull(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        var attributeSyntax = (AttributeSyntax) context.Node;

        // "attribute.Parent" is "AttributeListSyntax"
        // "attribute.Parent.Parent" is a C# fragment the attributes are applied to
        if (attributeSyntax.Parent?.Parent is not FieldDeclarationSyntax fieldDeclarationSyntax)
            return null;

        var goose = context.Node.Parent.Parent;
        var type = context.SemanticModel.GetDeclaredSymbol(fieldDeclarationSyntax) as ITypeSymbol;
        var name = fieldDeclarationSyntax.Declaration.Type.GetText();
        var shouldContinue = context.Node.ToString() is "InjectModuleService" or "InjectModuleService";
        if (!shouldContinue)
        {
            return null;
        }

        var res = new MyRet()
        {
            ServiceName = fieldDeclarationSyntax.Declaration.Type.GetText().ToString().Trim(),
            Namespace = context.SemanticModel.LookupNamespacesAndTypes(0).First().ToString(),
            Name = fieldDeclarationSyntax.Declaration.Variables[0].ToString(),
            ClassName = context.SemanticModel.GetDeclaredSymbol(context.Node.Parent.Parent!.Parent!)!.Name
        };
        return res;
    }
}

class MyRet
{
    public string Namespace { get; set; }
    public string Name { get; set; }
    public string ServiceName { get; set; }
    public string ClassName { get; set; }
}