namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class NegationNode : TreeNode
    {
        public override bool Evaluate()
        {
            return !Left!.Evaluate();
        }
    }
}
