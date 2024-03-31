//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Объявление символов алфавита сокращенного языка логики высказываний
// 27.03.2024
//
// Источники:
// - Логические основы интеллектуальных систем. Практикум
//

namespace LogicalExpressionClassLibrary
{
    public enum LogicalSymbols
    {
        Disjunction = '|',
        Conjunction = '&',
        LeftBracket = '(',
        RightBracket = ')',
        Equality = '~',
        //Implication = '→',
        Implication = '>',
        //Negation = '¬',
        Negation = '!',
        False = 'F',
        True = 'T',
    }
}
