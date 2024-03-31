﻿//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс узла синтаксического дерева для представления оператора "Эквивалентность" логики высказываний
// 27.02.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class EqualityNode(TreeNode? left, TreeNode? right) : TreeNode(left, right)
    {
        protected override bool Evaluate() => Left!.Evaluation == Right!.Evaluation;
        public override string ToString()
            => $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Equality}{Right}{(char)LogicalSymbols.RightBracket}";
    }
}
