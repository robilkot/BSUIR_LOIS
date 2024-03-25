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
