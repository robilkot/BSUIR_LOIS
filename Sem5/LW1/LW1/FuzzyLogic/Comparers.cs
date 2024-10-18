/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Классы, отвечающие за сравнение нечётких множеств: по носителю и по содержанию множеств
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
    public class SupportComparer : IEqualityComparer<(string Idtf, double Value)>
    {
        public bool Equals((string Idtf, double Value) x, (string Idtf, double Value) y)
            => x.Idtf.Equals(y.Idtf);

        public int GetHashCode([DisallowNull] (string Idtf, double Value) obj)
            => obj.Idtf.GetHashCode();
    }
    public class ContentComparer : IEqualityComparer<Fact>
    {
        public bool Equals(Fact? x, Fact? y)
            => x?.SequenceEqual(y) ?? false;

        public int GetHashCode([DisallowNull] Fact obj)
            => obj.GetHashCode();
    }
}
