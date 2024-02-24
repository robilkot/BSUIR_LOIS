namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    internal sealed class AtomicFormulaNode : TreeNode
    {
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!ValidateName(value))
                {
                    throw new ArgumentException("Invalid atomic formula");
                }

                _name = value;
            }
        }

        private bool _value = false;
        public bool Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    ClearEvaluation();
                    _value = value;
                }
            }
        }

        public AtomicFormulaNode(string name)
        {
            Name = name;
        }
        protected override bool Evaluate()
        {
            if(_parentExpression is null)
            {
                throw new InvalidOperationException("No context given for evaluation");
            }
            return _parentExpression.GetVariable(Name);
        }

        private static bool ValidateName(string name)
        {
            // First symbol must be a letter
            if (!name.Any() || !char.IsLetter(name[0]))
            {
                return false;
            }
            if (name.Length == 2 && name[1] == '0')
            {
                return false;
            }
            for (int i = 1; i < name.Length; i++)
            {
                if (!char.IsDigit(name[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
