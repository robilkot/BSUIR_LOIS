﻿//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс узла синтаксического дерева для представления константы "Истина" логики высказываний
// 27.02.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class TrueNode : TreeNode
    {
        private static readonly TrueNode _instance = new();
        private TrueNode() : base(null, null) { }
        public static TrueNode GetInstance() => _instance;
        protected override bool Evaluate() => true;
        public override string ToString() => $"{(char)LogicalSymbols.True}";
    }
}