using LogicalExpressionClassLibrary.Minimization.Strategy;

namespace LogicalExpressionClassLibrary.Minimization
{
    public static class Minimization
    {
        public static LogicalExpression Minimize(this LogicalExpression expr, NormalForms normalForm, IMinimizationStrategy strategy = null!)
        {
            strategy ??= new CombinedMinimizationStrategy();

            return strategy.Minimize(expr, normalForm);
        }
    }
}
