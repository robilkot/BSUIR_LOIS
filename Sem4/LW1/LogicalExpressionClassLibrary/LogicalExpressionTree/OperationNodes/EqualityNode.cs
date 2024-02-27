namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class EqualityNode : TreeNode
    {
        public EqualityNode(TreeNode? left, TreeNode? right) : base(left, right) { }
        protected override bool Evaluate()
        {
            return Left!.Evaluation == Right!.Evaluation;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Equality}{Right}{(char)LogicalSymbols.RightBracket}";
        }
    }
}
