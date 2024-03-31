//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс, отвечающий за логгирование информации в консоль
// 27.03.2024
//
// Источники:
// - Библиотека логгирования Serilog для C#
//

using System.Diagnostics;

namespace LogicalExpressionClassLibrary
{
    public static class ConsoleLogger
    {
        public static DebugLevels DebugLevel = 0;
        [Flags] public enum DebugLevels
        {
            Error = 1, 
            Warning = 2,  
            Info = 4, 
            Debug = 8
        }
        public static void Log(string message, DebugLevels level = DebugLevels.Info)
        {
            if(DebugLevel.HasFlag(level))
            {
                var className = new StackTrace().GetFrame(1)?.GetMethod()?.ReflectedType!.Name;

                Console.WriteLine($"[{level}] [{className}] {message}");
            }
        }
    }
}
