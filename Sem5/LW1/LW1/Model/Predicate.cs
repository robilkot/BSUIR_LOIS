/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за представление нечёткого множества
// 31.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

namespace LW1.Model
{
    public class Predicate(string name, Dictionary<string, double> pairs) : Dictionary<string, double>(pairs)
    {
        public string Name { get; set; } = name;

        public override string ToString()
        {
            var strings = this.Select(pair => $"<{pair.Key}, {pair.Value}>");

            return $"{{{string.Join(", ", strings)}}}";
        }

        new public double this[string variable]
        {
            get
            {
                TryGetValue(variable, out var value);
                return value;
            }
            set
            {
                if (!TryAdd(variable, value))
                {
                    base[variable] = value;
                }
            }
        }
    }

    public static class PredicateExtensions
    {
        public static Predicate ToPredicate(this IEnumerable<(string, double)> pairs, string name)
            => new(name, pairs.ToDictionary());
        public static Predicate ToPredicate(this Dictionary<string, double> pairs, string name)
            => new(name, pairs);

        public static Predicate WithName(this Predicate fact, string name)
        {
            fact.Name = name;
            return fact;
        }
    }
}
