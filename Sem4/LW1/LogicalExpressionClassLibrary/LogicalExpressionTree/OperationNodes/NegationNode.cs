//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс узла синтаксического дерева для представления оператора "Отрицание" логики высказываний
// 27.02.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

using System.Xml.Linq;

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class NegationNode(TreeNode? left) : TreeNode(left, null)
    {
        protected override bool Evaluate() => !Left!.Evaluation;
        public override string ToString()
            => $"{(char)LogicalSymbols.LeftBracket}{(char)LogicalSymbols.Negation}{Left}{(char)LogicalSymbols.RightBracket}";
    }
}
