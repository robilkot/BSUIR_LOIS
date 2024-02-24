using LogicalExpressionClassLibrary.LogicalExpressionTree;

namespace LogicalExpressionClassLibrary
{
    public class LogicalExpression
    {
        private readonly Dictionary<string, bool> _variables = [];
        // todo: implement after everything else is done to speed up evaluation.
        // maybe move this to treenodes?
        // also perform benchmark
        // also check for 32 variables (c)
        //private readonly Dictionary<TreeNode, bool> _cachedEvaluations = [];

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

            return _root.Evaluate();
        }
        public bool GetVariable(string varName)
        {
            if (_variables.TryGetValue(varName, out bool variableValue) == true)
            {
                return variableValue;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }
        public void SetVariable(string varName, bool variableValue)
        {
            _variables[varName] = variableValue;
        }

        public void ClearVariables()
        {
            _variables.Clear();
        }
        private TreeNode BuildExpressionTree(string input)
        {
            throw new NotImplementedException();
        }
    }
}
