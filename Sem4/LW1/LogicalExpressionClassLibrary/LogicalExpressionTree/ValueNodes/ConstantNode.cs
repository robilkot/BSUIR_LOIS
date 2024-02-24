namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    internal sealed class ConstantNode : TreeNode
    {
        public bool Value = false;
        public override bool Evaluate()
        {
            return Value;
        }
    }
}
