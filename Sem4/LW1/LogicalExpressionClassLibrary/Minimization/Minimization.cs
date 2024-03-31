using LogicalExpressionClassLibrary.Minimization.Strategy;

namespace LogicalExpressionClassLibrary.Minimization
{
    public static class Minimization
    {
        public static LogicalExpression Minimize(this LogicalExpression expr, NormalForms normalForm, IMinimizationStrategy strategy = null!)
        {
            strategy ??= new CombinedStrategy();

            var toReturn = strategy.Minimize(expr, normalForm);

            ConsoleLogger.Log($"Minimized expression: {toReturn}", ConsoleLogger.DebugLevels.Info);

            return toReturn;
        }
    }
}
