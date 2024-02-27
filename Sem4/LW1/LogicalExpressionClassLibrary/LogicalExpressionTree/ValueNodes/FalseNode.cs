namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class FalseNode : TreeNode
    {
        private static readonly FalseNode _instance = new();
        private FalseNode() : base(null, null) { }
        public static FalseNode GetInstance()
        {
            return _instance;
        }
        protected override bool Evaluate()
        {
            return false;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.False}";
        }
    }
}
