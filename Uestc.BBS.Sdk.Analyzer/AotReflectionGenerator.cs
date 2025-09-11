using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Uestc.BBS.Sdk.Analyzer
{
    [Generator]
    public class AotReflectionGenerator : IIncrementalGenerator
    {
        /// <summary>
        /// 获取访问权限的字面量
        /// </summary>
        /// <param name="accessibility"></param>
        /// <returns></returns>
        private static string GetAccessibilityLiteral(Accessibility accessibility) =>
            accessibility switch
            {
                Accessibility.Private => "private",
                Accessibility.ProtectedAndInternal => "protected internal",
                Accessibility.Protected => "protected",
                Accessibility.Internal => "internal",
                Accessibility.Public => "public",
                _ => string.Empty,
            };

        /// <summary>
        /// 获取类型最简化名称
        /// </summary>
        /// <param name="typeSymbol"></param>
        /// <returns></returns>
        private string GetFullyQualifiedName(ITypeSymbol typeSymbol)
        {
            if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
            {
                var elementType = GetFullyQualifiedName(arrayTypeSymbol.ElementType);
                return $"{elementType}[]";
            }
            else if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                return namedTypeSymbol.ToDisplayString().Split('.').Last();
            }
            else
            {
                return typeSymbol.ToString();
            }
        }

        /// <summary>
        /// 获取类的 using 指令
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> GetUsingDirectives(SyntaxNode node)
        {
            var usingDirectives = new List<string>();
            var compilationUnit = node.AncestorsAndSelf()
                .OfType<CompilationUnitSyntax>()
                .FirstOrDefault();
            if (compilationUnit != null)
            {
                foreach (var usingDirective in compilationUnit.Usings)
                {
                    usingDirectives.Add(usingDirective.ToString());
                }
            }
            return usingDirectives;
        }

        public void Initialize(IncrementalGeneratorInitializationContext initContext)
        {
            // 获取所有包含 ObjectFuncAttribute 的类型
            var classInfoProvider = initContext.SyntaxProvider.ForAttributeWithMetadataName(
                "Uestc.BBS.Sdk.Attributes.AotReflectionAttribute",
                (syntaxNode, _) => true,
                (syntaxContext, _) =>
                    (
                        syntaxContext.TargetSymbol,
                        syntaxContext.SemanticModel,
                        syntaxContext.TargetNode
                    )
            );

            // 为每个类生成代码
            initContext.RegisterSourceOutput(
                classInfoProvider,
                (sourceProductionContext, source) =>
                {
                    if (source.TargetSymbol is not INamedTypeSymbol classTypeSymbol)
                    {
                        return;
                    }

                    var sb = new StringBuilder();
                    // 添加 using 指令
                    var usingDirectives = GetUsingDirectives(source.TargetNode);
                    foreach (var usingDirective in usingDirectives)
                    {
                        sb.AppendLine(usingDirective);
                    }
                    sb.AppendLine();
                    sb.AppendLine($"namespace {classTypeSymbol.ContainingNamespace}");
                    sb.AppendLine("{");
                    sb.AppendLine(
                        $"    {GetAccessibilityLiteral(classTypeSymbol.DeclaredAccessibility)} partial class {classTypeSymbol.Name}"
                    );
                    sb.AppendLine("    {");
                    // Dictionary<string, Type> PropertyTypes
                    sb.AppendLine(
                        "        public static Dictionary<string, Type> PropertyTypes { get; } = new()"
                    );
                    sb.AppendLine("        {");
                    // 遍历所有属性
                    var propertySymbols = classTypeSymbol
                        .GetMembers()
                        .OfType<IPropertySymbol>()
                        .Where(p => p.DeclaredAccessibility == Accessibility.Public);
                    foreach (var propertySymbol in propertySymbols)
                    {
                        sb.AppendLine(
                            $"            {{nameof({propertySymbol.Name}), typeof({GetFullyQualifiedName(propertySymbol.Type)})}},"
                        );
                    }
                    sb.AppendLine("        };");
                    sb.AppendLine();
                    // object GetValue(string name)
                    sb.AppendLine("        public object GetValue(string name) => name switch");
                    sb.AppendLine("        {");
                    foreach (var propertySymbol in propertySymbols)
                    {
                        sb.AppendLine(
                            $"            nameof({propertySymbol.Name}) => {propertySymbol.Name},"
                        );
                    }
                    sb.AppendLine("            _ => throw new ArgumentException(nameof(name))");
                    sb.AppendLine("        };");
                    sb.AppendLine("    }");
                    sb.AppendLine("}");
                    sourceProductionContext.AddSource(
                        $"{classTypeSymbol.ContainingNamespace}.{classTypeSymbol.Name}.g.cs",
                        sb.ToString()
                    );
                }
            );
        }
    }
}
