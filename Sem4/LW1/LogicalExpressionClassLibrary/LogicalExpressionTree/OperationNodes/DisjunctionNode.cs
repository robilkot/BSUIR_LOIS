namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class DisjunctionNode : TreeNode
    {
        public override bool Evaluate()
        {
            return Left!.Evaluate() && Right!.Evaluate();
        }
    }
}
