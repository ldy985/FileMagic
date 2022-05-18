using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ldy985.RuleListGenerator;

public class ControllerFinder : ISyntaxReceiver
{
    public List<string> Controllers { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not ClassDeclarationSyntax controller)
            return;

        if (controller.BaseList == null)
            return;

        bool any = false;

        foreach (BaseTypeSyntax baseTypeSyntax in controller.BaseList.Types)
        {
            string sourceText = baseTypeSyntax.Type.GetText().ToString();
            if (sourceText.StartsWith("BaseRule") || sourceText.StartsWith("IRule"))
                any = true;
        }

        if (any)
            Controllers.Add(controller.Identifier.Text);
    }
}