namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class NegationNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return !Left!.Evaluation;
        }
    }
}
