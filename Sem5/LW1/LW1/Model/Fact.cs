namespace LW1.Model
{
    public class Fact(string name, IEnumerable<(string Idtf, double Value)> pairs) : List<(string Idtf, double Value)>(pairs)
    {
        public string Name { get; set; } = name;

        public override string ToString()
        {
            var strings = this.Select(pair => $"<{pair.Idtf}, {pair.Value}>");

            return $"{{{string.Join(", ", strings)}}}";
        }
    }

    public static class FactExtensions
    {
        public static Fact ToFact(this IEnumerable<(string Idtf, double Value)> pairs, string name)
            => new (name, pairs);
    }
}
