namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class TrueNode : TreeNode
    {
        private static readonly TrueNode _instance = new();
        private TrueNode() : base(null, null) { }
        public static TrueNode GetInstance()
        {
            return _instance;
        }
        protected override bool Evaluate()
        {
            return true;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.True}";
        }
    }
}
