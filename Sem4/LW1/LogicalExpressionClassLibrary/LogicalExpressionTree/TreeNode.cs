namespace LogicalExpressionClassLibrary.LogicalExpressionTree
{
    public abstract class TreeNode
    {
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
        public void ClearEvaluation()
        {
            _evaluation = null;
            // If value is outdated then upper tree part is also outdated
            Parent?.ClearEvaluation();
        }
    }
}
