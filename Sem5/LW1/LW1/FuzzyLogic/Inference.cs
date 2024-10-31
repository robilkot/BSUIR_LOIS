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
using System.Collections;
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

        public static IEnumerable<Predicate> WhereSupportEquals(this IEnumerable<Predicate> sets, Predicate sample)
            => [.. sets.Where(set => set.Keys.All(sample.ContainsKey) && set.Count == sample.Count)];

        public static Predicate Infer(Predicate set1, Predicate set2, Predicate set3)
            => set1.Composition(set2.Implication(set3));

        public static Predicate Composition(this Predicate set, List<(string Src, string Trg, double Value)> rule)
            => set
                .SelectMany(pair => rule
                            .Where(tuple => tuple.Trg == pair.Key)
                            .Select(t => (t.Src, t.Trg, t.Value))
                            .Log())
                .GroupBy(g => g.Trg)
                .Select(g => (g.Key, g.Min(g1 => g1.Value)))
                .ToPredicate(set.Name);

        public static IEnumerable<string> Run(KB kb)
        {
            var previousFactsCount = 0;

            do
            {
                previousFactsCount = kb.Facts.Count;

                foreach (var rule in kb.Rules)
                {
                    var set2 = kb.Facts.First(f => f.Name == rule.Item1);
                    var set3 = kb.Facts.First(f => f.Name == rule.Item2);

                    var inferred = kb.Facts
                        .WhereSupportEquals(set3)
                        .Select(set1 => (set1, Infer(set1, set2, set3).WithName($"I{kb.Facts.Count}")))
                        //.Where(tuple => !kb.Facts.Contains(tuple.Item2, new ContentComparer())) // Dont add duplicates
                        .Select(tuple =>
                        {
                            if (!kb.Facts.Contains(tuple.Item2, new ContentComparer()))
                            {
                                kb.Facts.Add(tuple.Item2);
                            }
                            else
                            {
                                tuple.Item2.Name = "  ";
                            }

                            return $"{tuple.Item2.Name} = {tuple.set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {tuple.Item2}";
                        });

                    foreach (var fact in inferred)
                    {
                        yield return fact;
                    }
                }
            }
            while (previousFactsCount != kb.Facts.Count);
        }

        public static T Log<T>(this T obj, Func<T, string>? formatter = null)
        {
            formatter ??= (t) => $"{t}";

            if (obj is IEnumerable en)
            {
                foreach (var o in en)
                    Console.WriteLine(o);
            }
            else
            {
                Console.WriteLine(formatter(obj));
            }

            return obj;
        }
    }
}
