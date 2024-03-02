namespace LogicalExpressionClassLibrary.LogicalExpressionTree
{
    public abstract class TreeNode
    {
        [Flags] public enum DebugLevels 
        {
            Clear = 0,
            Eval = 1,
            Set = 2
        }
        public static DebugLevels DebugLevel = 0;
        private TreeNode? _right = null;
        public TreeNode? Right
        {
            get => _right;
            set
            {
                ClearEvaluation();
                _right = value;
            }
        }
        private TreeNode? _left = null;
        public TreeNode? Left
        {
            get => _left;
            set
            {
                ClearEvaluation();
                _left = value;
            }
        }
        public TreeNode? Parent { get; set; } = null;
        // Serves caching purpose and is used in truth tables
        private bool? _evaluation = null;
        public bool Evaluation
        {
            get
            {
                if (_evaluation == null)
                {
                    _evaluation = Evaluate();

                    if ((DebugLevel & DebugLevels.Eval) != 0) Console.WriteLine($"[evl] {ToString()}"); // - {GetType().Name}");
                }
                return _evaluation.Value;
            }
        }
        public TreeNode(TreeNode? left, TreeNode? right)
        {
            Right = right;
            Left = left;
        }
        protected abstract bool Evaluate();
        protected void ClearEvaluation()
        {
            if (_evaluation != null)
            {
                if ((DebugLevel & DebugLevels.Clear) != 0)
                {
                    Console.Write($"[clr] {GetType().Name}");

                    if (Parent != null)
                    {
                        Console.Write($" with parent {Parent}\n");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                _evaluation = null;
                // If value is outdated then upper tree part is also outdated
                Parent?.ClearEvaluation();
            }
        }
    }
}
