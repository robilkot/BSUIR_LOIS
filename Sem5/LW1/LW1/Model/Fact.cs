namespace LW1.Model
{
    public class Fact(string name) : List<(string, double)>
    {
        public string Name { get; init; } = name;
    }
}
