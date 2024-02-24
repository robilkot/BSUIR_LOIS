namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class TrueNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return true;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.True}";
        }
    }
}
