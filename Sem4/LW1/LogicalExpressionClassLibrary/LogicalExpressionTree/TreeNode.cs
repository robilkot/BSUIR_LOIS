namespace LogicalExpressionClassLibrary.LogicalExpressionTree
{
    internal abstract class TreeNode
    {
        // Needed to keep context for evaluating variables and stuff
        protected LogicalExpression? _parentExpression = null;
        public TreeNode? Right { get; set; } = null;
        public TreeNode? Left { get; set; } = null;
        public abstract bool Evaluate();
        public void SetParentExpression(LogicalExpression? parentExpression)
        {
            _parentExpression = parentExpression;

            Right?.SetParentExpression(parentExpression);
            Left?.SetParentExpression(parentExpression);
        }
    }
}
