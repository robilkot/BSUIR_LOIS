using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;

namespace LogicalExpressionClassLibrary
{
    public class LogicalExpression
    {
        private readonly Dictionary<string, AtomicFormulaNode> _variables = [];

        private TreeNode? _root = null;

        public LogicalExpression(string input)
        {
            _root = BuildExpressionTree(input);
        }

        public bool Evaluate()
        {
            if (_root is null)
            {
                throw new InvalidOperationException("Expression is not set");
            }

            return _root.Evaluation;
        }
        public bool GetVariable(string varName)
        {
            if (_variables.TryGetValue(varName, out var variable) == true)
            {
                return variable.Value;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }
        public void SetVariable(string varName, bool variableValue)
        {
            if(_variables.TryGetValue(varName, out var variable))
            {
                variable.Value = variableValue;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }

        public override string ToString()
        {
            return _root != null ? string.Empty : _root!.ToString()!;
        }
        private TreeNode BuildExpressionTree(string input)
        {
            throw new NotImplementedException();
        }
    }
}
