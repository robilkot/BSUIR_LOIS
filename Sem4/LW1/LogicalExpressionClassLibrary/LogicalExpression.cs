﻿using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using LogicalExpressionClassLibrary.LogicalParser;
using LogicalExpressionClassLibrary.Minimization.Strategy;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LogicalExpressionClassLibrary
{
    public sealed partial class LogicalExpression
    {
        public static bool Debug = false;

        private readonly Dictionary<string, List<AtomicFormulaNode>> _variables = [];
        public int VariablesCount => _variables.Count;
        private TreeNode? _root;
        public TreeNode? Root { get { return _root; }
            set
            {
                //if(_root is not null)
                //{
                //    throw new Exception("Can't change existing expression root");
                //} else
                {
                    _root = value;
                }
            }
        }

        private List<Dictionary<string, bool>>? _truthTable;
        public List<Dictionary<string, bool>> TruthTable
        {
            get
            {
                _truthTable ??= BuildTruthTable();
                return new(_truthTable);
            }
        }
        private int? _numberForm;
        public int NumberForm
        {
            get
            {
                _numberForm ??= CalculateNumberForm();
                return _numberForm.Value;
            }
        }
        public bool Evaluation => _root!.Evaluation;
        private LogicalExpression? _fdnf;
        public LogicalExpression FDNF
        {
            get
            {
                if (_fdnf is null)
                {
                    _fdnf = BuildNormalForm(NormalForms.FDNF);
                    _fdnf._fdnf = _fdnf;
                }
                return _fdnf;
            }
        }
        private LogicalExpression? _fcnf;
        public LogicalExpression FCNF
        {
            get
            {
                if (_fcnf is null)
                {
                    _fcnf = BuildNormalForm(NormalForms.FCNF);
                    _fcnf._fcnf = _fcnf;
                }
                return _fcnf;
            }
        }
        public LogicalExpression(string input, AbstractLogicalParser logicalParser = null!)
        {
            if (input.Length == 0)
            {
                throw new ArgumentException("Expression notation is empty");
            }

            // Inject new parser if provided
            logicalParser = logicalParser is null ? new RecursiveLogicalParser() : logicalParser;

            (_root, _variables) = logicalParser.Parse(input);
        }
        private LogicalExpression() { }
        public static LogicalExpression Empty { get => new(); }
        private TreeNode BuildNormalFormOperand(Dictionary<string, List<AtomicFormulaNode>> variables, Dictionary<string, bool> row, NormalForms strategy)
        {
            TreeNode? root = null;

            foreach (var variable in _variables!)
            {
                var atomicFormulaNode = new AtomicFormulaNode(variable.Value[0]);

                string atomicFormulaNodeString = atomicFormulaNode.ToString();

                if (variables.TryGetValue(atomicFormulaNodeString, out var list))
                {
                    list.Add(atomicFormulaNode);
                }
                else
                {
                    variables.Add(atomicFormulaNodeString, [atomicFormulaNode]);
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
        public string ToNFNumericString(NormalForms strategy)
        {
            void ProcessSubtree(TreeNode root, out int numericValue)
            {
                numericValue = 0;
                int twosPower = 0;

                Type targetType = strategy switch
                {
                    NormalForms.FDNF => typeof(ConjunctionNode),
                    NormalForms.FCNF => typeof(DisjunctionNode),
                    _ => throw new ArgumentException("Invalid strategy specified"),
                };
                while (root.GetType() == targetType)
                {
                    if (root.Right is AtomicFormulaNode ^ strategy == NormalForms.FCNF)
                    {
                        numericValue += (int)Math.Pow(2, twosPower);
                    }
                    twosPower++;

                    root = root.Left!;
                }

                // Last variable is a left leaf, not right
                if (root is AtomicFormulaNode ^ strategy == NormalForms.FCNF)
                {
                    numericValue += (int)Math.Pow(2, twosPower);
                }
            }

            StringBuilder builder = new();
            builder.Append('(');

            var currentNode = _root;

            Type targetType = strategy switch
            {
                NormalForms.FDNF => typeof(DisjunctionNode),
                NormalForms.FCNF => typeof(ConjunctionNode),
                _ => throw new ArgumentException("Invalid strategy specified"),
            };
            while (currentNode!.GetType() == targetType)
            {
                ProcessSubtree(currentNode.Right!, out int numericValue);

                builder.Append(numericValue).Append(", ");

                currentNode = currentNode.Left;
            }

            ProcessSubtree(currentNode!, out int lastNumericValue);
            builder.Append(lastNumericValue)
                    .Append(") ")
                    .Append($"{(strategy == NormalForms.FDNF ? (char)LogicalSymbols.Disjunction : (char)LogicalSymbols.Conjunction)}");

            return builder.ToString();
        }
        public LogicalExpression Minimize(NormalForms normalForm, IMinimizeStrategy strategy = null!)
        {
            strategy ??= new EvaluationMinimizeStrategy();

            return strategy.Minimize(this, normalForm);
        }
        private int CalculateNumberForm()
        {
            int numberForm = 0;

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
                numberForm += bit ? (int)Math.Pow(2, twosPower) : 0;
                twosPower++;
            }

            if (Debug)
            {
                Console.WriteLine($"[idx] Index form: {Convert.ToString(numberForm, 2)}");
            }

            return numberForm;
        }
        private LogicalExpression BuildNormalForm(NormalForms strategy)
        {
            LogicalExpression NormalForm = new();

            foreach (var truthRow in TruthTable.Where(r => r[ToString()] ^ strategy == NormalForms.FCNF))
            {
                var newOperand = BuildNormalFormOperand(NormalForm._variables, truthRow, strategy);

                // Append new operand to already built tree
                TreeNode newRoot = NormalForm._root switch
                {
                    null => newOperand,
                    not null when strategy == NormalForms.FCNF => new ConjunctionNode(NormalForm._root, newOperand),
                    not null when strategy == NormalForms.FDNF => new DisjunctionNode(NormalForm._root, newOperand),
                    _ => throw new ArgumentException("Invalid strategy specified"),
                };

                if (NormalForm._root is not null)
                {
                    NormalForm._root!.Parent = newRoot;
                }
                NormalForm._root = newRoot;
            }

            if (NormalForm._root is null)
            {
                throw new InvalidOperationException($"Cannot build {strategy}");
            }

            return NormalForm;
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
        public override string ToString() => _root?.ToString()!;

        [ExcludeFromCodeCoverage]
        public string ToTruthTableString()
        {
            const int MaxColumnTitleWidth = 20;

            StringBuilder builder = new();

            foreach (var k in TruthTable[0].Keys)
            {
                builder.Append($"| {k[..Math.Min(k.Length, MaxColumnTitleWidth)]} ");
            }
            builder.AppendLine("| Total |");

            var separator = string.Empty.PadRight(builder.Length - 2, '-') + '\n';

            builder.Insert(0, separator).Append(separator);

            foreach (var truthRow in TruthTable)
            {
                foreach (var kvp in truthRow)
                {
                    string v = kvp.Value ? $"| {(char)LogicalSymbols.True} " : $"| {(char)LogicalSymbols.False} ";

                    builder.Append(v.PadRight(Math.Min(kvp.Key.Length, MaxColumnTitleWidth) + 3));
                }

                var expressionValue = truthRow[ToString()];
                builder.AppendLine(expressionValue ? $"|   {(char)LogicalSymbols.True}   |" : $"|   {(char)LogicalSymbols.False}   |");
            }

            builder.Append(separator);

            return builder.ToString();
        }
        public bool IsTautology()
        {
            string currentExpressionNotation = ToString();

            return !TruthTable.Any(row => row[currentExpressionNotation] == false);
        }
        public bool ImpliesFrom(LogicalExpression source)
        {
            LogicalExpression implication = new($"({source}→{this})");

            return implication.IsTautology();
        }
    }
}
