﻿//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс узла синтаксического дерева для представления атомарной формулы логики высказываний
// 27.02.2024
//
// Источники:
// - Основы Алгоритмизации и Программирования (2 семестр). Практикум
//

namespace LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes
{
    public sealed class AtomicFormulaNode : TreeNode
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (!ValidateName(value))
                {
                    throw new ArgumentException("Invalid atomic formula");
                }

                _name = value;
            }
        }

        private bool _value = false;
        public bool Value

        {
            get => _value;
            set
            {
                ConsoleLogger.Log($"Set {Name} to {value}", ConsoleLogger.DebugLevels.Debug);
                
                if (_value != value)
                {
                    ClearEvaluation();
                    _value = value;
                }
            }
        }

        public AtomicFormulaNode(string name) : base(null, null)
        {
            Name = name;
        }
        protected override bool Evaluate() => Value;

        private static bool ValidateName(string name)
        {
            // First symbol must be a letter
            if (name.Length == 0 || !char.IsLetter(name[0]))
            {
                return false;
            }
            if (name.Length > 1)
            {
                if (name[1] == '0')
                {
                    return false;
                }

                for (int i = 1; i < name.Length; i++)
                {
                    if (!char.IsDigit(name[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override string ToString() => Name;
    }
}