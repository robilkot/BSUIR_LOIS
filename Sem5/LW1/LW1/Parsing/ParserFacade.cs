/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за инкапсуляцию деталей парсера базы знаний. Реализует паттерн проектирования "фасад"
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

using Antlr4.Runtime;
using LW1.Model;

namespace LW1.Parsing
{
    public class ParserFacade
    {
        private readonly FuzzyLogicParser _parser;
        private readonly ExceptionListener _exceptionListener = new();
        private readonly KBVisitor _visitor = new();

        public ParserFacade(string text)
        {
            AntlrInputStream _input_stream = new(text);
            FuzzyLogicLexer _lexer = new(_input_stream);
            CommonTokenStream _stream = new(_lexer);
            _parser = new(_stream);

            _parser.AddErrorListener(_exceptionListener);
        }

        public KB Parse()
            => _parser.kb().Accept(_visitor);
    }
}
