namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class FalseNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return false;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.False}";
        }
    }
}
