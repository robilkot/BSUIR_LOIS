/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за представление базы знаний с фактами и правилами
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

using System.Collections;

namespace LW1.Model
{
    public class KB : IEnumerable<string>
    {
        public List<Predicate> Facts { get; set; } = [];
        public List<Rule> Rules { get; set; } = [];

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var fact in Facts)
                yield return $"{fact}";

            foreach (var fact in Rules)
                yield return $"{fact}";
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
