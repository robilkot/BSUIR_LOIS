using LW1.Model;
using System.Diagnostics.CodeAnalysis;

namespace LW1.FuzzyLogic
{
    public class CoreComparer : IEqualityComparer<(string Idtf, double Value)>
    {
        public bool Equals((string Idtf, double Value) x, (string Idtf, double Value) y)
            => x.Idtf.Equals(y.Idtf);

        public int GetHashCode([DisallowNull] (string Idtf, double Value) obj)
            => obj.Idtf.GetHashCode();
    }
    public class FullComparer : IEqualityComparer<Fact>
    {
        public bool Equals(Fact? x, Fact? y)
            => x?.SequenceEqual(y) ?? false;

        public int GetHashCode([DisallowNull] Fact obj)
            => obj.GetHashCode(); // todo check
    }

    public static class Inference
    {
        private static readonly CoreComparer s_coreComparer = new();
        private static readonly FullComparer s_fullComparer = new();

        public static double TNorm(double a, double b)
            => Math.Min(a, b);
        public static List<(string Src, string Trg, double)> Implication(this Fact A, Fact B)
            => [..A.SelectMany(a => B.Select(b => (a.Idtf, b.Idtf, a.Value > b.Value ? b.Value : 1)))];

        public static List<Fact> FindFittingSets(Fact sample, List<Fact> sets)
            => [.. sets.Where(set => sample.SequenceEqual(set, s_coreComparer))];

        public static Fact Infer(Fact set1, Fact set2, Fact set3)
            => set1.And(set2.Implication(set3));

        public static Fact And(this Fact set1, List<(string Src, string Trg, double)> matrix)
        {
            
            List<(string Idtf1, string Idtf2, double Value)> data = [];

            foreach (var (Idtf, Value) in set1)
            {
                foreach(var cell in matrix)
                {
                    if(cell.Src == Idtf)
                    {
                        data.Add((cell.Src, cell.Trg, TNorm(cell.Item3, Value)));
                    }
                }
            }

            Dictionary<string, double> max_third_elements = [];
            
            foreach (var (_, Idtf2, Value) in data)
            {
                double? secondValue = null;

                try
                {
                    secondValue = max_third_elements[Idtf2];
                }
                catch(KeyNotFoundException e)
                {

                }
                finally { }

                if (!max_third_elements.ContainsKey(Idtf2) || (secondValue is not null && Value > secondValue))
                {
                    if (!max_third_elements.TryAdd(Idtf2, Value))
                    {
                        max_third_elements[Idtf2] = Value;
                    };
                }
            }

            var filtered = data
                .Where(e => max_third_elements.ContainsKey(e.Idtf2))
                .Where(e => e.Value == max_third_elements[e.Idtf2])
                .Select(e => (e.Idtf2, e.Value))
                .Distinct()
                .ToList();

            return new(set1.Name, filtered);
        }


        public static List<string> Run(KB kb, bool show_duplicates = false)
        {
            var sets = kb.Facts;
            var rules = kb.Rules;

            var old_size = 0;
            var new_size = 1;

            List<string> results = [];

            while (old_size != new_size)
            {
                old_size = sets.Count;

                foreach (var rule in rules)
                {
                    var set2 = sets.FirstOrDefault(f => f.Name == rule.Item1);
                    var set3 = sets.FirstOrDefault(f => f.Name == rule.Item2);

                    var sets1 = FindFittingSets(set2, sets);

                    foreach (var set1 in sets1)
                    {
                        var inference = Infer(set1, set2, set3);
                        string new_name = $"I{sets.Count}";

                        inference.Name = new_name;

                        if (sets.Contains(inference, s_fullComparer))
                        {
                            if (show_duplicates)
                            {
                                results.Add($"# {set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inference}");
                            }
                            continue;
                        }

                        sets.Add(inference);

                        results.Add($"{new_name} = {set1.Name}/~\\({rule.Item1}~>{rule.Item2}) = {inference}");
                    }

                }

                new_size = sets.Count;
            }

            return results;
        }

    }
}
