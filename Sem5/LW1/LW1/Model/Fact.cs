﻿/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за представление нечёткого множества
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

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

        public static Fact WithName(this Fact fact, string name)
        {
            fact.Name = name;
            return fact;
        }
    }
}
