using System.Diagnostics;
using static LogicalExpressionClassLibrary.AppConstants;

namespace LogicalExpressionClassLibrary
{
    public class Benchmarks
    {
        static string buildTest(int varCount, LogicalSymbols op)
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
                result = result.Insert(0, '('.ToString());
                result = result += LogicalSymbolsDict[op] + symbols[i] + ')';
            }

            return result;
        }

        public static void Execute()
        {
            Stopwatch sw = new();

            List<int> cases = [1, 4, 7, 10, 15, 20, 23, 26];

            foreach (int testCase in cases)
            {
                LogicalExpression expr0 = new(buildTest(testCase, LogicalSymbols.Conjunction));
                LogicalExpression expr1 = new(buildTest(testCase, LogicalSymbols.Equality));

                sw.Restart();
                Console.WriteLine($"{expr0} " + (expr0.ImpliesFrom(expr1) ? "implies" : "doesn't imply") + $" from {expr1}");
                sw.Stop();
                Console.WriteLine($"{testCase} VARIABLES. ELAPSED {sw.Elapsed}");
            }
        }
    }
}
