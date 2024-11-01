/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за осуществление нечёткого логического вывода, а также операции нахождения треугольной нормы и импликации нечётких множеств
// 01.11.2024
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
        public static double TNorm(double a, double b)
            => Math.Min(a, b);

        public static double GoedelImplication(double a, double b)
            => a > b ? b : 1;

        public static List<(string Src, string Trg, double)> Implication(this Predicate A, Predicate B)
            => [.. A.SelectMany(a => B.Select(b => (a.Key, b.Key, GoedelImplication(a.Value, b.Value))))];

        public static IEnumerable<Predicate> WhereSupportEquals(this IEnumerable<Predicate> predicates, Predicate sample)
            => [.. predicates.Where(set => set.Keys.All(sample.ContainsKey) && set.Count == sample.Count)];

        public static Predicate Infer(Predicate p1, Predicate p2, Predicate p3)
            => p1.Composition(p2.Implication(p3));

        public static Predicate Composition(this Predicate predicate, List<(string Src, string Trg, double Value)> rule)
            => predicate
                .SelectMany(pair => rule
                            .Where(tuple => tuple.Src == pair.Key)
                            .Select(t => (t.Src, t.Trg, t.Value)))
                .GroupBy(tuple => tuple.Trg)
                .Select(group => (group.Key, group.Max(g2 => TNorm(predicate[g2.Src], g2.Value))))
                .ToPredicate(predicate.Name);

        public static IEnumerable<string> Run(KB kb, bool showDuplicates = false)
        {
            int old_size = 0;
            int new_size = 1;

            List<string> results = [];

            while (old_size != new_size)
            {
                old_size = kb.Facts.Count;

                foreach (var rule in kb.Rules)
                {
                    var p2 = kb.Facts.First(f => f.Name == rule.Item1);
                    var p3 = kb.Facts.First(f => f.Name == rule.Item2);
                    var predicates = kb.Facts.WhereSupportEquals(p2);

                    foreach (var p1 in predicates)
                    {
                        var new_name = $"I{kb.Facts.Count}";
                        var inferred = Infer(p1, p2, p3).WithName(new_name);

                        if (kb.Facts.Contains(inferred, new ContentComparer()))
                        {
                            if (showDuplicates)
                            {
                                results.Add($"   = {p1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inferred}");
                            }

                            continue;
                        }

                        kb.Facts.Add(inferred);

                        results.Add($"{new_name} = {p1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inferred}");
                    }
                }

                new_size = kb.Facts.Count;
            }

            return results;
        }
    }
}
