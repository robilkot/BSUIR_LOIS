namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class DisjunctionNode : TreeNode
    {
        public DisjunctionNode(TreeNode? left, TreeNode? right) : base(left, right) { }
        protected override bool Evaluate()
        {
            return Left!.Evaluation || Right!.Evaluation;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Disjunction}{Right}{(char)LogicalSymbols.RightBracket}";
        }
    }
}
