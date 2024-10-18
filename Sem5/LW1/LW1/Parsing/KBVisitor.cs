/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за обработку лексем записи базы знаний и преобразование их в объекты классов системы. Реализует паттерн проектирования "посетитель"
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LW1.Model;

namespace LW1.Parsing
{
    public class KBVisitor : IFuzzyLogicVisitor<KB>
    {
        public KB VisitKb([NotNull] FuzzyLogicParser.KbContext context)
            => new()
            {
                Facts = [.. context.fact().Select(VisitFactInternal)],
                Rules = [.. context.rule().Select(VisitRuleInternal)]
            };

        private static Fact VisitFactInternal(FuzzyLogicParser.FactContext context)
            => new ($"{context.ID()}", context.pair().Select(p => ($"{p.ID()}", double.Parse($"{p.FLOAT()}"))));

        private static Rule VisitRuleInternal(FuzzyLogicParser.RuleContext context)
            => new($"{context.ID()[0]}", $"{context.ID()[1]}");

        #region Not implemented interface methods
        public KB Visit([NotNull] IParseTree tree)
            => throw new NotImplementedException();

        public KB VisitChildren([NotNull] IRuleNode node)
            => throw new NotImplementedException();

        public KB VisitErrorNode([NotNull] IErrorNode node)
            => throw new NotImplementedException();

        public KB VisitFact([NotNull] FuzzyLogicParser.FactContext context)
            => throw new NotImplementedException();

        public KB VisitPair([NotNull] FuzzyLogicParser.PairContext context)
            => throw new NotImplementedException();

        public KB VisitRule([NotNull] FuzzyLogicParser.RuleContext context)
            => throw new NotImplementedException();

        public KB VisitTerminal([NotNull] ITerminalNode node)
            => throw new NotImplementedException();
        #endregion
    }
}
