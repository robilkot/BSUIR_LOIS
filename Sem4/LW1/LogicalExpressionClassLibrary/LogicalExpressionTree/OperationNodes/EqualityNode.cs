namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class EqualityNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return Left!.Evaluation == Right!.Evaluation;
        }
    }
}
