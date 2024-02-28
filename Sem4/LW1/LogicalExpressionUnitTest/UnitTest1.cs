using LogicalExpressionClassLibrary;

namespace LogicalExpressionUnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("A123")]
        [InlineData("B")]
        [InlineData("C1")]
        [InlineData("((A1|(A2&A3))|B1)")]
        [InlineData("((A1|F)|T)")]
        [InlineData("(¬(¬A123))")]
        [InlineData("((A1|(B2&A3))|B1)")]
        public void Parsing_shouldEqual(string input)
        {
            LogicalExpression expr = new(input);

            Assert.Equal(input, expr.ToString());
        }
    }
}