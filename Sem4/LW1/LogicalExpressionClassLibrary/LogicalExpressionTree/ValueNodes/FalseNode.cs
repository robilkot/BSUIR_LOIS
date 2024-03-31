﻿//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс узла синтаксического дерева для представления константы "Ложь" логики высказываний
// 27.02.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class FalseNode : TreeNode
    {
        private static readonly FalseNode _instance = new();
        private FalseNode() : base(null, null) { }
        public static FalseNode GetInstance() => _instance;
        protected override bool Evaluate() => false;
        public override string ToString() => $"{(char)LogicalSymbols.False}";
    }
}