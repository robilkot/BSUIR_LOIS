using System.Xml.Linq;

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class NegationNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return !Left!.Evaluation;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.LeftBracket}{(char)LogicalSymbols.Negation}{Left}{(char)LogicalSymbols.RightBracket}";
        }
    }
}
