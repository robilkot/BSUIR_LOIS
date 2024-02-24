namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class DisjunctionNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return Left!.Evaluation && Right!.Evaluation;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Disjunction}{Right}{(char)LogicalSymbols.RightBracket}";
        }
    }
}
