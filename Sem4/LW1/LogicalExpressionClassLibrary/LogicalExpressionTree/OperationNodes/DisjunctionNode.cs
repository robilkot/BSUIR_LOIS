namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class DisjunctionNode : TreeNode
    {
        protected override bool Evaluate()
        {
            return Left!.Evaluation && Right!.Evaluation;
        }
    }
}
