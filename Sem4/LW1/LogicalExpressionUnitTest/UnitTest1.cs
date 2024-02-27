using LogicalExpressionClassLibrary;

namespace LogicalExpressionUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string input = "(A123)";

            LogicalExpression expr = new(input);

            //FalseNode node = new();

            //NegationNode node2 = new();
            //node2.Left = node;

            //NegationNode node3 = new();
            //node3.Left = node2;

            //var str = node3.ToString();

            //_ = LogicalExpression.BuildTruthTable();
        }
    }
}