using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using System.Collections;

namespace LogicalExpressionClassLibrary
{
    public class LogicalExpression
    {
        private readonly Dictionary<string, AtomicFormulaNode> _variables = [];

        private TreeNode? _root = null;
        private List<Dictionary<string, bool>>? _truthTable;
        public List<Dictionary<string, bool>> TruthTable
        {
            get
            {
                _truthTable ??= BuildTruthTable();
                return new(_truthTable);
            }
        }

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
            if (_variables.TryGetValue(varName, out var variable))
            {
                if (variableValue != variable.Value)
                {
                    variable.Value = variableValue;

                }
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

        private List<Dictionary<string, bool>> BuildTruthTable()
        {
            static void NextCombination(BitArray bits)
            {
                for (int i = bits.Length - 1; i >= 0; i--)
                {
                    bits[i] = !bits[i];

                    // If changed value was 0, no need update bits on left side from it
                    if (bits[i])
                    {
                        break;
                    }
                }
            }

            static void StoreEvaluation(Dictionary<string, bool> truthRow, TreeNode? root)
            {
                if(root is null)
                {
                    return;
                }

                truthRow[root.ToString()!] = root.Evaluation;

                StoreEvaluation(truthRow, root.Left);
                StoreEvaluation(truthRow, root.Right);
            }

            if (_root is null)
            {
                throw new InvalidOperationException("Expression is not set");
            }

            List<Dictionary<string, bool>> truthTable = [];

            // Preserve initial state before checking all combinations of variables
            // todo: Seems unoptimal but copying the whole tree is not a good idea either
            Dictionary<AtomicFormulaNode, bool> initialVariablesState = [];

            foreach(var v in _variables.Values)
            {
                initialVariablesState[v] = v.Value;
            }

            // Mask indicates which variables will be true
            BitArray variablesTruthMask = new(_variables.Count, false);

            while(!variablesTruthMask.HasAllSet())
            {
                int variableIndex = 0; 
                foreach(var v in _variables.Values)
                {
                    v.Value = variablesTruthMask[variableIndex];
                    variableIndex++;
                }

                Dictionary<string, bool> truthRow = [];

                StoreEvaluation(truthRow, _root);

                truthTable.Add(truthRow);

                NextCombination(variablesTruthMask);
            }

            // Revert to initial values
            foreach (var kv in initialVariablesState)
            {
                kv.Key.Value = kv.Value;
            }

            return truthTable;
        }
    }
}
