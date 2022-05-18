using System.Text;
using Microsoft.CodeAnalysis;

namespace ldy985.RuleListGenerator;

[Generator]
public class FileMagicRuleHelpersGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not ControllerFinder a)
            return;

        StringBuilder sb = new StringBuilder();

        sb.Append(@"using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core.Rules.Rules;
using ldy985.FileMagic.Core.Rules.Rules.Media;
using ldy985.FileMagic.Core.Rules.Rules.Fonts;
using ldy985.FileMagic.Core.Rules.Rules.Containers;
using ldy985.FileMagic.Core.Rules.Rules.Containers.Archive;
using ldy985.FileMagic.Core.Rules.Rules.FileSystems;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace ldy985.FileMagic.Core.Rules;

public static partial class FileMagicRuleHelpers
{
    public static IRule[] GetDefaultFileMagicRules(ILoggerFactory? loggerFactory = null)
    {
        loggerFactory ??= NullLoggerFactory.Instance;
        ");

        sb.AppendLine($"var result = new IRule[{a.Controllers.Count}];");

        int i = 0;

        foreach (string classDeclarationSyntax in a.Controllers)
        {
            sb.Append("    ");
            sb.Append("    ");
            sb.AppendLine($"result[{i++}] = CreateRule<{classDeclarationSyntax}>(loggerFactory);");
        }

        sb.Append("    ");
        sb.Append("    ");
        sb.AppendLine("return result;");

        sb.Append("    ");
        sb.Append('}').AppendLine();
        sb.Append('}');

        context.AddSource("FileMagicRuleHelpers.g.cs", sb.ToString());
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ControllerFinder());
    }
}