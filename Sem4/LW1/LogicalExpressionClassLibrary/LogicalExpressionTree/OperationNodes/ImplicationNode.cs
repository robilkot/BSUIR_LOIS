namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    internal sealed class ImplicationNode : TreeNode
    {
        protected override bool Evaluate()
        {
            if(Left!.Evaluation && !Right!.Evaluation)
            {
                return false;
            }
            return true;
        }
    }
}
