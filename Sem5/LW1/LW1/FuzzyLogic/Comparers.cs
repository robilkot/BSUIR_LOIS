/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Классы, отвечающие за сравнение нечётких множеств по их содержанию
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
//

using LW1.Model;
using System.Diagnostics.CodeAnalysis;

namespace LW1.FuzzyLogic.Comparers
{
    public class ContentComparer : IEqualityComparer<Fact>
    {
        public bool Equals(Fact? x, Fact? y)
            => y is not null 
            && x is not null 
            && x.All(kvp => y.TryGetValue(kvp.Key, out var comp) && comp == kvp.Value) 
            && x.Count == y.Count;

        public int GetHashCode([DisallowNull] Fact obj) => obj.GetHashCode();
    }
}
