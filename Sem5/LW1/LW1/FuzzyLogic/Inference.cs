/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за осуществление нечёткого логического вывода, а также операции нахождения треугольной нормы и импликации нечётких множеств
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

using LW1.FuzzyLogic.Comparers;
using LW1.Model;
using System.Data;

namespace LW1.FuzzyLogic
{
    public static class Inference
    {
        private static readonly SupportComparer s_supportComparer = new();
        private static readonly ContentComparer s_fullComparer = new();

        public static double TNorm(double a, double b)
            => Math.Min(a, b);

        public static List<(string Src, string Trg, double)> Implication(this Fact A, Fact B)
            => [.. A.SelectMany(a => B.Select(b => (a.Idtf, b.Idtf, a.Value > b.Value ? b.Value : 1)))];

        public static IEnumerable<Fact> WhereSupportEquals(this IEnumerable<Fact> sets, Fact sample)
            => [.. sets.Where(set => sample.SequenceEqual(set, s_supportComparer))];

        public static Fact Infer(Fact set1, Fact set2, Fact set3)
            => set1.Composition(set2.Implication(set3));

        // Max-min composition
        public static Fact Composition(this Fact set, List<(string Src, string Trg, double)> rule)
            => set
            .SelectMany(pair
                => rule
                .Where(cell => cell.Src == pair.Idtf)
                .Select(cell
                    => (cell.Src, cell.Trg, TNorm(cell.Item3, pair.Value))))
            .GroupBy(tuple => tuple.Trg)
            .Select(g => (g.Key, g.Max(tuple => tuple.Item3)))
            .ToFact(set.Name);

        public static IEnumerable<string> Run(KB kb)
        {
            var facts = kb.Facts;
            var rules = kb.Rules;

            var previousFactsCount = 0;

            do
            {
                previousFactsCount = facts.Count;

                foreach (var rule in rules)
                {
                    var set2 = facts.First(f => f.Name == rule.Item1);
                    var set3 = facts.First(f => f.Name == rule.Item2);

                    foreach (var set1 in facts.WhereSupportEquals(set2))
                    {
                        var inferred = Infer(set1, set2, set3).WithName($"I{facts.Count}");

                        if (facts.Contains(inferred, s_fullComparer))
                        {
                            continue;
                        }

                        facts.Add(inferred);

                        yield return $"{inferred.Name} = {set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inferred}";
                    }
                }
            }
            while (previousFactsCount != facts.Count);
        }
    }
}
