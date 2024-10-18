/////////////////////////////////////////////////////////////////
// Практическая работа №1 по дисциплине ЛОИС
// Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Основной код программы, отвечающий за считывание данных из файла и запуск нечеткого логического вывода
// 18.10.2024
//
// Источники:
// - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
// - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
// - Getting Started With ANTLR in C# / https://tomassetti.me/getting-started-with-antlr-in-csharp/
//

using LW1.FuzzyLogic;
using LW1.Parsing;

string workingDirectory = Environment.CurrentDirectory;
string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

using var file = File.OpenText($"{projectDirectory}/Input/example1.kb");

string kbString = file.ReadToEnd();

try
{
    var kb = new ParserFacade(kbString).Parse();

    var res = Inference.Run(kb);

    foreach (var item in res)
    {
        Console.WriteLine(item);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}