using static LogicalExpressionClassLibrary.LogicalExpression;

namespace LogicalExpressionClassLibrary.Minimization.Strategy
{
    public class TableMinimizeStrategy : IMinimizeStrategy
    {
        public LogicalExpression Minimize(LogicalExpression input, NormalForms form)
        {
            LogicalExpression source = form switch
            {
                NormalForms.FCNF => input.FCNF,
                NormalForms.FDNF => input.FDNF,
                _ => throw new NotImplementedException()
            };

            source = source.MergeConstituents(form);

            //

            return source;
        }
    }
}
