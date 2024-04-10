﻿//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс парсера строковой записи логического выражения, использующий рекурсивный принцип работы
// 10.04.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using static LogicalExpressionClassLibrary.AppConstants;

namespace LogicalExpressionClassLibrary.LogicalParser
{
    public sealed class RecursiveLogicalParser : AbstractLogicalParser
    {
        private readonly Dictionary<string, List<AtomicFormulaNode>> _variables = [];
        public override (TreeNode, Dictionary<string, List<AtomicFormulaNode>>) Parse(string input)
        {
            _variables.Clear();

            (int i, int leftBracketsCount, int rightBracketsCount) refs = (0, 0, 0);

            var root = BuildFormulaTree(input, ref refs, null);

            return (root, new(_variables));
        }

        private TreeNode BuildFormulaTree(string input, ref (int i, int leftBracketsCount, int rightBracketsCount) refs, TreeNode? parentNode)
        {
            TreeNode toReturn = null!;

            for (; refs.i < input.Length; refs.i++)
            {
                if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.LeftBracket][0])
                {
                    refs.leftBracketsCount++;
                    refs.i++;

                    toReturn = BuildFormulaTree(input, ref refs, toReturn);
                }
                else if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.RightBracket][0])
                {
                    refs.rightBracketsCount++;
                    break;
                }
                else if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.False][0])
                {
                    toReturn = toReturn is null
                        ? _constNodes[LogicalSymbols.False]
                        : throw new ArgumentException($"Unexpected symbol '{input[refs.i]}'");
                }
                else if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.True][0])
                {
                    toReturn = toReturn is null
                        ? _constNodes[LogicalSymbols.True]
                        : throw new ArgumentException($"Unexpected symbol '{input[refs.i]}'");
                }

                else if (char.IsLetter(input[refs.i]))
                {
                    if (char.IsLower(input[refs.i]))
                    {
                        throw new ArgumentException("Variable must be an uppercase letter");
                    }

                    if (toReturn is not null)
                    {
                        throw new ArgumentException($"Unexpected variable '{input[refs.i]}'");
                    }

                    string variableName = input[refs.i].ToString();
                    //refs.i++;

                    var atomicFormulaNode = new AtomicFormulaNode(variableName);

                    // Store variable name association with node for expression
                    if (_variables.TryGetValue(variableName, out var alreadyCreatedList))
                    {
                        alreadyCreatedList.Add(atomicFormulaNode);
                    }
                    else
                    {
                        _variables.Add(variableName, [atomicFormulaNode]);
                    }

                    toReturn = atomicFormulaNode;
                }

                // Encountered symbol is unary (negation) operator
                else if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.Negation][0])
                {
                    if (toReturn is not null)
                    {
                        throw new ArgumentException($"Negation operator can't follow another operand ('{toReturn}')");
                    }

                    refs.i++;

                    toReturn = new NegationNode(null);
                    toReturn.Left = BuildFormulaTree(input, ref refs, toReturn);

                    break;
                }

                // Binary operators
                else
                {
                    if (toReturn == null)
                    {
                        string exceptionCommentary = input.Insert(refs.i, "__?__");
                        throw new ArgumentException($"Missing operand before '{input[refs.i]}': {exceptionCommentary}");
                    }

                    LogicalSymbols binaryOperator;

                    if (input[refs.i] == LogicalSymbolsDict[LogicalSymbols.Equality][0])
                    {
                        binaryOperator = LogicalSymbols.Equality;
                    }
                    else if (refs.i < input.Length - 1)
                    {
                        binaryOperator = input[refs.i] switch
                        {
                            '\\' when input[refs.i + 1] == '/' => LogicalSymbols.Disjunction,
                            '/' when input[refs.i + 1] == '\\' => LogicalSymbols.Conjunction,
                            '-' when input[refs.i + 1] == '>' => LogicalSymbols.Implication,
                            _ => throw new ArgumentException($"Unexpected token '{input[refs.i]}' in expression notation")
                        };
                    }
                    else throw new ArgumentException($"Unexpected token '{input[refs.i]}' in expression notation");

                    refs.i += LogicalSymbolsDict[binaryOperator].Length;

                    var newNode = _binaryOperatorsNodes[binaryOperator](toReturn, null!);

                    toReturn.Parent = newNode;

                    toReturn = newNode;

                    toReturn.Right = BuildFormulaTree(input, ref refs, toReturn);

                    break;
                }
            }

            // Brackets validation
            int bracketsCountDifference = refs.leftBracketsCount - refs.rightBracketsCount;
            if (bracketsCountDifference != 0 && parentNode is null && refs.i == input.Length - 1)
            {
                throw new ArgumentException($"Invalid indentation ({Math.Abs(bracketsCountDifference)} brackets {(bracketsCountDifference > 0 ? "less" : "more")} than expected)");
            }
            //else if (toReturn is AtomicFormulaNode || toReturn is TrueNode || toReturn is FalseNode)
            //{
            //    if (refs.leftBracketsCount + refs.rightBracketsCount != 0)
            //    {
            //        throw new ArgumentException($"Invalid indentation (Simple formulas must not contain brackets)");
            //    }
            //}
            //else if(refs.leftBracketsCount + refs.rightBracketsCount == 0)
            //{
            //    throw new ArgumentException($"Invalid indentation (Complex formulas must contain brackets)");
            //}


            if (toReturn == null)
            {
                string exceptionCommentary = input.Insert(refs.i, "__?__");
                throw new ArgumentException($"Missing token in expression notation: {exceptionCommentary}");
            }
            toReturn.Parent = parentNode;

            return toReturn;
        }
    }
}
