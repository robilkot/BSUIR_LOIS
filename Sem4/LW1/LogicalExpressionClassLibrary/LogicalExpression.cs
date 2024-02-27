﻿using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using System.Collections;

namespace LogicalExpressionClassLibrary
{
    public class LogicalExpression
    {
        private static readonly Dictionary<LogicalSymbols, Func<TreeNode, TreeNode, TreeNode>> _binaryOperatorsNodes = new()
            {
                { LogicalSymbols.Conjunction, (left, right) => new ConjunctionNode(left, right) },
                { LogicalSymbols.Disjunction, (left, right) => new DisjunctionNode(left, right) },
                { LogicalSymbols.Implication, (left, right) => new ImplicationNode(left, right) },
                { LogicalSymbols.Equality,    (left, right) => new EqualityNode(left, right) },
            };

        private static readonly Dictionary<LogicalSymbols, Func<TreeNode>> _constNodes = new()
            {
                { LogicalSymbols.True, () => TrueNode.GetInstance() },
                { LogicalSymbols.False, () => FalseNode.GetInstance() },
            };

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
            int runningIndex = 0;

            _root = BuildFormulaTree(input, ref runningIndex);
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
            return _root == null ? string.Empty : _root!.ToString()!;
        }
        private TreeNode BuildFormulaTree(string input, ref int i)
        {
            static string extractVariableName(string input, ref int runningIndex)
            {
                int beginIndex = runningIndex;

                if (input[++runningIndex] == '0')
                {
                    throw new ArgumentException("Atomic formula's first digit can't be zero");
                }

                while (runningIndex < input.Length && char.IsDigit(input[runningIndex]))
                {
                    runningIndex++;
                }

                // Account for outer increment in parser for loop
                runningIndex--;

                return input[beginIndex..(runningIndex + 1)];
            }

            Stack<TreeNode> stack = new();
            int indent = 0;

            for (; i < input.Length; i++)
            {
                // Encountered symbol is variable or const
                if (char.IsLetter(input[i]))
                {
                    var variableName = extractVariableName(input, ref i);

                    if (variableName.Length == 1 && Enum.IsDefined(typeof(LogicalSymbols), (int)variableName[0]))
                    {
                        var nodeGeneratingFunc = _constNodes[(LogicalSymbols)variableName[0]];

                        stack.Push(nodeGeneratingFunc());
                    }
                    else
                    {
                        var atomicFormulaNode = new AtomicFormulaNode(variableName);

                        // Store variable name association with node for expression
                        _variables[variableName] = atomicFormulaNode;

                        stack.Push(atomicFormulaNode);
                    }
                }

                // Encountered symbol is unary (negation) operator
                else if (input[i] == (char)LogicalSymbols.Negation)
                {
                    i++;

                    // todo: what if empty input after negation
                    var childFormula = BuildFormulaTree(input, ref i);

                    return new NegationNode(childFormula);
                }

                // Encountered symbol is binary operator
                else if (_binaryOperatorsNodes.TryGetValue((LogicalSymbols)input[i], out var nodeGenerator))
                {
                    if (stack.Count == 0)
                    {
                        string exceptionCommentary = input.Insert(i, "__?__");
                        throw new ArgumentException($"Missing operand before '{input[i]}' (index {i}): {exceptionCommentary}");
                    }

                    i++;

                    var left = stack.Pop();
                    var right = BuildFormulaTree(input, ref i);

                    return nodeGenerator(left, right);
                }

                else if (input[i] == (char)LogicalSymbols.LeftBracket)
                {
                    indent++;

                    if (indent > 0)
                    {
                        i++;

                        var newNode = BuildFormulaTree(input, ref i);

                        stack.Push(newNode);
                    }
                }
                else if (input[i] == (char)LogicalSymbols.RightBracket)
                {
                    indent--;

                    // Stop parsing inner expression
                    if (indent <= 0)
                    {
                        break;
                    }
                }
            }

            return stack.Pop();
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

            if (_root is null)
            {
                throw new InvalidOperationException("Expression is not set");
            }

            List<Dictionary<string, bool>> truthTable = [];

            // Preserve initial state before checking all combinations of variables
            // todo: Seems unoptimal but copying the whole tree is not a good idea either
            Dictionary<AtomicFormulaNode, bool> initialVariablesState = [];

            foreach (var v in _variables.Values)
            {
                initialVariablesState[v] = v.Value;
            }

            // Mask indicates which variables will be true
            BitArray variablesTruthMask = new(_variables.Count, false);

            while (!variablesTruthMask.HasAllSet())
            {
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

            // Revert to initial values
            foreach (var kv in initialVariablesState)
            {
                kv.Key.Value = kv.Value;
            }

            return truthTable;
        }
    }
}
