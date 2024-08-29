using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace LibRoton.CodeGen;

[Generator(LanguageNames.CSharp)]
public class SerializerGenerator : IIncrementalGenerator
{
    private const string SerializerAttributeCode = @"
namespace LibRoton.Generated
{
    [System.AttributeUsage(System.AttributeTargets.Struct)]
    public class GenerateSerializerAttribute : System.Attribute
    {
    }
}
";

    public class Serializer
    {
        
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "GenerateSerializerAttribute.g.cs",
            SourceText.From(SerializerAttributeCode, Encoding.UTF8)));

        IncrementalValuesProvider<Serializer?> toGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "LibRoton.Generated.GenerateSerializerAttribute",
                predicate: static (s, _) => true,
                transform: static (ctx, _) => GetEnumToGenerate(ctx.SemanticModel, ctx.TargetNode))
            .Where(static m => m is not null);
        
        // Generate source code for each enum found
        context.RegisterSourceOutput(toGenerate,
            static (spc, source) => Execute(source, spc));
    }

    private static Serializer? GetEnumToGenerate(SemanticModel semanticModel, SyntaxNode targetNode)
    {
        return null;
    }
    
    static void Execute(Serializer? enumToGenerate, SourceProductionContext context)
    {
        // if (enumToGenerate is { } value)
        // {
        //     // generate the source code and add it to the output
        //     string result = SourceGenerationHelper.GenerateExtensionClass(value);
        //     // Create a separate partial class file for each enum
        //     context.AddSource($"EnumExtensions.{value.Name}.g.cs", SourceText.From(result, Encoding.UTF8));
        // }
    }
}