using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using LogicalExpressionClassLibrary.LogicalParser;
using System.Collections;
using System.Text;

namespace LogicalExpressionClassLibrary
{
    public sealed class LogicalExpression
    {
        public static bool Debug = false;
        private enum NormalForms
        {
            FCNF,
            FDNF
        }

        private AbstractLogicalParser _logicalParser = new RecursiveLogicalParser();
        private Dictionary<string, List<AtomicFormulaNode>>? _variables;
        private TreeNode? _root;

        private List<Dictionary<string, bool>>? _truthTable;
        public List<Dictionary<string, bool>> TruthTable
        {
            get
            {
                _truthTable ??= BuildTruthTable();
                return new(_truthTable);
            }
        }
        private int? _indexForm;
        public int IndexForm
        {
            get
            {
                _indexForm ??= CalculateIndexForm();
                return _indexForm.Value;
            }
        }
        public bool Evaluation
        {
            get => _root!.Evaluation;
        }
        public LogicalExpression(string input, AbstractLogicalParser logicalParser = null!)
        {
            // Inject new parser if provided
            _logicalParser = logicalParser is null ? _logicalParser : logicalParser;

            if (input.Length == 0)
            {
                throw new ArgumentException("Expression notation is empty");
            }

            (_root, _variables) = _logicalParser.Parse(input);
        }
        private LogicalExpression() { }
        private TreeNode buildNormalFormTree(LogicalExpression NF, Dictionary<string, bool> row, NormalForms strategy)
        {
            TreeNode? root = null;

            foreach (var variable in _variables!)
            {
                var atomicFormulaNode = new AtomicFormulaNode(variable.Value[0]);

                string atomicFormulaNodeString = atomicFormulaNode.ToString();

                if (NF._variables!.TryGetValue(atomicFormulaNodeString, out var list))
                {
                    list.Add(atomicFormulaNode);
                }
                else
                {
                    NF._variables!.Add(atomicFormulaNodeString, [atomicFormulaNode]);
                }

                TreeNode toAppend = null!;

                // Invert logic if calculating FCNF
                if (row[variable.Key] ^ (strategy == NormalForms.FCNF))
                {
                    toAppend = atomicFormulaNode;
                }
                else
                {
                    var negationNode = new NegationNode(atomicFormulaNode);
                    atomicFormulaNode.Parent = negationNode;
                    toAppend = negationNode;
                }

                if (root is null)
                {
                    root = toAppend;
                }
                else
                {
                    TreeNode newRoot = strategy switch
                    {
                        NormalForms.FDNF => new ConjunctionNode(root, toAppend),
                        NormalForms.FCNF => new DisjunctionNode(root, toAppend),
                        _ => throw new ArgumentException("Invalid strategy specified"),
                    };
                    toAppend.Parent = newRoot;
                    root.Parent = newRoot;
                    root = newRoot;
                }
            }

            return root!;

        }
        public LogicalExpression ToFCNF()
        {
            LogicalExpression FCNF = new()
            {
                _variables = [],
            };

            foreach (var truthRow in TruthTable)
            {
                if (!truthRow[ToString()])
                {
                    var newConjunct = buildNormalFormTree(FCNF, truthRow, NormalForms.FCNF);
                    // Append new conjunct to already built tree
                    TreeNode newRoot = FCNF._root is null ? newConjunct : new ConjunctionNode(FCNF._root, newConjunct);
                    if (FCNF._root is not null)
                    {
                        FCNF._root!.Parent = newRoot;
                    }
                    FCNF._root = newRoot;
                }
            }

            return FCNF;
        }
        public LogicalExpression ToFDNF()
        {
            LogicalExpression FDNF = new()
            {
                _variables = [],
            };

            foreach (var truthRow in TruthTable)
            {
                if (truthRow[ToString()])
                {
                    var newConjunct = buildNormalFormTree(FDNF, truthRow, NormalForms.FDNF);
                    // Append new disjunct to already built tree
                    TreeNode newRoot = FDNF._root is null ? newConjunct : new DisjunctionNode(FDNF._root, newConjunct);
                    if (FDNF._root is not null)
                    {
                        FDNF._root!.Parent = newRoot;
                    }
                    FDNF._root = newRoot;
                }
            }

            return FDNF;
        }

        public bool GetVariable(string varName)
        {
            if (_variables!.TryGetValue(varName, out var variable) == true && variable.Count > 0)
            {
                return variable[0].Value;
            }
            else throw new ArgumentException($"Uninitialized variable '{varName}' addressed");
        }
        public void SetVariable(string varName, bool variableValue)
        {
            if (_variables!.TryGetValue(varName, out var variableList))
            {
                foreach (var variable in variableList)
                {
                    variable.Value = variableValue;
                }
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
            Dictionary<string, bool> initialVariablesState = [];

            foreach (var kvp in _variables!)
            {
                initialVariablesState[kvp.Key] = kvp.Value[0].Value;
            }

            // Mask indicates which variables will be true
            BitArray variablesTruthMask = new(_variables.Count, false);

            do
            {
                if (Debug)
                {
                    Console.WriteLine("[nxt] Next combination of values");
                }

                int variableIndex = 0;

                foreach (var v in _variables.Values)
                {
                    foreach (var node in v)
                    {
                        node.Value = variablesTruthMask[variableIndex];
                    }

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
                Console.WriteLine("[nxt] Reverting to default values");
            }

            foreach (var kv in initialVariablesState)
            {
                var listOfNodesToReset = _variables[kv.Key];

                foreach (var node in listOfNodesToReset)
                {
                    node.Value = kv.Value;
                }
            }

            return truthTable;
        }

        private int CalculateIndexForm()
        {
            int indexForm = 0;

            List<bool> bitList = [];

            foreach (var truthRow in TruthTable)
            {
                var expressionValue = truthRow[ToString()];
                bitList.Add(expressionValue);
            }

            bitList.Reverse();

            int twosPower = 0;

            foreach (var bit in bitList)
            {
                indexForm += bit ? (int)Math.Pow(2, twosPower) : 0;
                twosPower++;
            }

            if (Debug)
            {
                Console.WriteLine($"[idx] Index form: {Convert.ToString(indexForm, 2)}");
            }

            return indexForm;
        }
        public string ToTruthTableString()
        {
            StringBuilder builder = new();

            foreach (var k in TruthTable[0].Keys)
            {
                builder.Append($"| {k} ");
            }
            builder.AppendLine("| Total |");

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
                builder.AppendLine(expressionValue ? "|   1   |" : "|   0   |");
            }

            builder.Append(separator);

            return builder.ToString();
        }
    }
}
