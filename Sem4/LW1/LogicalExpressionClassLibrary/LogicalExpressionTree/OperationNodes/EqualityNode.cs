namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class EqualityNode : TreeNode
    {
        public override bool Evaluate()
        {
            return Left!.Evaluate() == Right!.Evaluate();
        }
    }
}
