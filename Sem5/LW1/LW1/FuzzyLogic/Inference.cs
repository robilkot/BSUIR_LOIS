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
        {
            var x = set
                .SelectMany(pair => rule
                            .Where(tuple => tuple.Src == pair.Key)
                            .Select(t => (t.Src, t.Trg, t.Value))
                )
                .GroupBy(tuple => tuple.Trg);

            Dictionary<string, double> result = [];

            foreach (var g in x)
            {
                result.Add(g.Key, 0);

                //Console.WriteLine(g.Key);

                double max = 0;

                foreach (var g2 in g)
                {
                    double inPred = set[g2.Src];
                    double inRule = g2.Value;
                    //Console.WriteLine($"pred {inPred} rule {inRule}");
                    double norm = TNorm(inPred, inRule);
                    
                    if (norm > max)
                    {
                        max = norm;
                    }
                }

                result[g.Key] = max;
            }

            return result.ToPredicate(set.Name);
        }

        public static IEnumerable<string> Run(KB kb)
        {
            bool show_duplicates = false;

            int old_size = 0;
            int new_size = 1;

            List<string> results = [];

            while (old_size != new_size)
            {
                old_size = kb.Facts.Count;

                foreach (var rule in kb.Rules)
                {
                    var set2 = kb.Facts.First(f => f.Name == rule.Item1);
                    var set3 = kb.Facts.First(f => f.Name == rule.Item2);
                    var sets1 = kb.Facts.WhereSupportEquals(set2);

                    foreach (var set1 in sets1)
                    {
                        //Console.WriteLine($"{set1.Name} {set2.Name} {set3.Name}");
                        var new_name = $"I{kb.Facts.Count}";
                        var inference = Infer(set1, set2, set3);

                        if (kb.Facts.Contains(inference, new ContentComparer()))
                        {
                            if (show_duplicates)
                            {
                                results.Add($"   = {set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inference}");
                            }

                            continue;
                        }

                        inference.Name = new_name;
                        kb.Facts.Add(inference);

                        results.Add($"{new_name} = {set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inference}");
                    }
                }

                new_size = kb.Facts.Count;
            }

            return results;
        }

        public static T Log<T>(this T obj, Func<object, string>? formatter = null)
        {
            formatter ??= (t) => $"{t}";

            if (obj is IEnumerable en)
            {
                foreach (var o in en)
                    Console.WriteLine(formatter(o));
            }
            else
            {
                Console.WriteLine(formatter(obj));
            }

            return obj;
        }
    }
}
