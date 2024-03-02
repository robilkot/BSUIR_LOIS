using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using LogicalExpressionClassLibrary.LogicalParser;
using System.Collections;
using System.Text;

namespace LogicalExpressionClassLibrary
{
    public class LogicalExpression
    {
        public static bool Debug = false;

        private readonly AbstractLogicalParser _logicalParser;
        private readonly TreeNode? _root;
        private readonly Dictionary<string, AtomicFormulaNode>? _variables;

        private List<Dictionary<string, bool>>? _truthTable;
        public List<Dictionary<string, bool>> TruthTable
        {
            get
            {
                _truthTable ??= BuildTruthTable();
                return new(_truthTable);
            }
        }
        public LogicalExpression(string input, AbstractLogicalParser logicalParser = null!)
        {
            // Set default parser or inject new one
            _logicalParser = logicalParser is null ? new RecursiveLogicalParser() : logicalParser;

            if (input.Length == 0)
            {
                return;
            }

            (_root, _variables) = _logicalParser.Parse(input);
        }

        public bool Evaluate() => _root!.Evaluation;
        public bool GetVariable(string varName)
        {
            if (_variables!.TryGetValue(varName, out var variable) == true)
            {
                return variable.Value;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }
        public void SetVariable(string varName, bool variableValue)
        {
            if (_variables!.TryGetValue(varName, out var variable))
            {
                variable.Value = variableValue;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }

        public override string ToString()
        {
            return _root == null ? string.Empty : _root!.ToString()!;
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
                if (root is null)
                {
                    return;
                }

                truthRow[root.ToString()!] = root.Evaluation;

                StoreEvaluation(truthRow, root.Left);
                StoreEvaluation(truthRow, root.Right);
            }

            List<Dictionary<string, bool>> truthTable = [];

            // Preserve initial state before checking all combinations of variables
            Dictionary<AtomicFormulaNode, bool> initialVariablesState = [];

            foreach (var v in _variables!.Values)
            {
                initialVariablesState[v] = v.Value;
            }

            // Mask indicates which variables will be true
            BitArray variablesTruthMask = new(_variables.Count, false);

            do
            {
                if (Debug)
                {
                    Console.WriteLine("\n[nxt] Next combination of values");
                }

                int variableIndex = 0;

                foreach (var v in _variables.Values)
                {
                    v.Value = variablesTruthMask[variableIndex];
                    variableIndex++;
                }

                Dictionary<string, bool> truthRow = [];

                StoreEvaluation(truthRow, _root);

                truthTable.Add(truthRow);

                NextCombination(variablesTruthMask);

            }
            while (variablesTruthMask.HasAnySet());

            if (Debug)
            {
                Console.WriteLine("\n[nxt] Reverting to default values");
            }

            foreach (var kv in initialVariablesState)
            {
                kv.Key.Value = kv.Value;
            }

            return truthTable;
        }
        public string ToTruthTableString()
        {
            StringBuilder builder = new();

            foreach (var k in TruthTable[0].Keys)
            {
                builder.Append($"| {k} ");
            }
            builder.AppendLine("|");

            var separator = string.Empty.PadRight(builder.Length - 2, '-') + '\n';

            builder.Insert(0, separator);
            builder.Append(separator);

            foreach (var truthRow in TruthTable)
            {
                foreach (var kvp in truthRow)
                {
                    string v = kvp.Value ? "| 1 " : "| 0 ";

                    builder.Append(v.PadRight(kvp.Key.Length + 3));
                }

                var expressionValue = truthRow[ToString()];
                builder.AppendLine(expressionValue ? "| T" : "|");
            }

            builder.Append(separator);

            return builder.ToString();
        }
    }
}
