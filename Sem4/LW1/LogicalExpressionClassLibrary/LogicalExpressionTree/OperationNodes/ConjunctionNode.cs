namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class ConjunctionNode : TreeNode
    {
        public override bool Evaluate()
        {
            // todo: maybe null forgiving operator shouldn't be here.
            // depends on how much we trust the correctness of logical expression
            // same concern for all binary operations
            return Left!.Evaluate() || Right!.Evaluate();
        }
    }
}
