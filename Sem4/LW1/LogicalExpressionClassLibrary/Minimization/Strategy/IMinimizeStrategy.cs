using static LogicalExpressionClassLibrary.LogicalExpression;

namespace LogicalExpressionClassLibrary.Minimization.Strategy
{
    public interface IMinimizeStrategy
    {
        public LogicalExpression Minimize(LogicalExpression input, NormalForms form);
    }
}
