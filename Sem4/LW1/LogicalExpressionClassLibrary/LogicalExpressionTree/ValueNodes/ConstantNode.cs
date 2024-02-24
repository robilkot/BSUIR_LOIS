namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    internal sealed class ConstantNode : TreeNode
    {
        private bool _value = false;
        public bool Value
        {
            get { return _value; }
            set
            {
                if (_value != value) {
                    ClearEvaluation();
                    _value = value;
                }
            }
        }
        protected override bool Evaluate()
        {
            return Value;
        }
    }
}
