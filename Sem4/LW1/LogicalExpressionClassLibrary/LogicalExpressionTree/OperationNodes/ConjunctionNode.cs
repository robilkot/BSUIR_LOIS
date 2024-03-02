namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class ConjunctionNode(TreeNode? left, TreeNode? right) : TreeNode(left, right)
    {
        // todo: maybe null forgiving operator shouldn't be here.
        // depends on how much we trust the correctness of logical expression
        // same concern for all binary operations
        protected override bool Evaluate() => Left!.Evaluation && Right!.Evaluation;
        public override string ToString()
            => $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Conjunction}{Right}{(char)LogicalSymbols.RightBracket}";
    }
}
