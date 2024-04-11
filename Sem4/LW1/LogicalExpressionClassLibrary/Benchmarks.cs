using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static LogicalExpressionClassLibrary.AppConstants;

namespace LogicalExpressionClassLibrary
{
    [ExcludeFromCodeCoverage]
    public class Benchmarks
    {
        static string BuildTest(int varCount, LogicalSymbols op)
        {
            List<char> symbols = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

            if (varCount < 1 && varCount > symbols.Count)
            {
                throw new NotImplementedException();
            }

            string result = string.Empty;

            result += symbols[0];

            for (int i = 1; i < varCount; i++)
            {
                result = result.Insert(0, LogicalSymbolsDict[LogicalSymbols.LeftBracket]);
                result = result += LogicalSymbolsDict[op] + symbols[i] + LogicalSymbolsDict[LogicalSymbols.RightBracket];
            }

            return result;
        }

        public static void Execute()
        {
            Stopwatch sw = new();

            List<int> cases = [1, 4, 7, 10, 15, 20, 23, 26];
            
            foreach (int testCase in cases)
            {
                LogicalExpression expr0 = new(BuildTest(testCase, LogicalSymbols.Conjunction));
                LogicalExpression expr1 = new(BuildTest(testCase, LogicalSymbols.Equality));

                sw.Restart();
                Console.WriteLine($"{expr0} " + (expr0.ImpliesFrom(expr1) ? "implies" : "doesn't imply") + $" from {expr1}");
                sw.Stop();
                Console.WriteLine($"{testCase} VARIABLES. ELAPSED {sw.Elapsed}");
            }
        }
    }
}
