namespace LogicalExpressionClassLibrary.LogicalExpressionTree
{
    internal abstract class TreeNode
    {
        // Needed to keep context for evaluating variables and stuff
        protected LogicalExpression? _parentExpression = null;
        private TreeNode? _right = null;
        public TreeNode? Right
        {
            get
            {
                return _right;
            }
            set
            {
                ClearEvaluation();
                _right = value;
            }
        }
        private TreeNode? _left = null;
        public TreeNode? Left
        {
            get
            {
                return _left;
            }
            set
            {
                ClearEvaluation();
                _left = value;
            }
        }
        public TreeNode? Parent { get; set; } = null;
        // Serves caching purpose and will be used in truth tables
        private bool? _evaluation = null;
        public bool Evaluation
        {
            get
            {
                _evaluation ??= Evaluate();
                return (bool)_evaluation;
            }
        }
        protected abstract bool Evaluate();
        public void SetParentExpression(LogicalExpression? parentExpression)
        {
            _parentExpression = parentExpression;

            Right?.SetParentExpression(parentExpression);
            Left?.SetParentExpression(parentExpression);
        }
        public void ClearEvaluation()
        {
            _evaluation = null;
            // If value is outdated then upper tree part is also outdated
            Parent?.ClearEvaluation();
        }
    }
}
