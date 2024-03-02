using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalParser;

namespace LogicalExpressionUnitTest
{
    public class LogicalExpressionUnitTest
    {
        [Theory]
        [InlineData("A123")]
        [InlineData("B")]
        [InlineData("C1")]
        [InlineData("((A1|(A2&A3))|B1)")]
        [InlineData("((A1|F)|T)")]
        [InlineData("(¬(¬A123))")]
        [InlineData("(¬(¬(¬(¬A123))))")]
        [InlineData("((A1|(B2&A3))|B1)")]
        public void RecursiveParsing_shouldEqual(string input)
        {
            LogicalExpression expr = new(input, new RecursiveLogicalParser());

            Assert.Equal(input, expr.ToString());
        }

        [Theory]
        [InlineData("((A1|A2)|A3)", true, true, true, true)]
        [InlineData("((A1|A2)|A3)", false, false, false, false)]
        [InlineData("((A1&A2)|A3)", false, false, true, true)]
        [InlineData("((A1&A2)|A3)", true, true, false, true)]
        [InlineData("((A1&A2)&A3)", true, false, false, false)]
        [InlineData("((A1&A2)&A3)", true, true, true, true)]
        public void Evaluation_shouldEqualExprected(string input, bool A1, bool A2, bool A3, bool result)
        {
            LogicalExpression expr = new(input);
            expr.SetVariable("A1", A1);
            expr.SetVariable("A2", A2);
            expr.SetVariable("A3", A3);

            var actualResult = expr.Evaluate();

            Assert.Equal(result, actualResult);
        }

        [Theory]
        [InlineData("(1)")]
        [InlineData("(A023)")]
        [InlineData("(A1B)")]
        [InlineData("(A1=B1)")]
        [InlineData("(A1&)")]
        [InlineData("(¬)")]
        [InlineData("(A1&(|C1))")]
        public void Parsing_IncorrectNotation_shouldThrow(string input)
        {
            Assert.Throws<ArgumentException>(() => new LogicalExpression(input));
        }
    }
}