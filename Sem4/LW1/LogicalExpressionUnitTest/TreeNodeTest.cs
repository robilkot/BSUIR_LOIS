using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalExpressionTree.ValueNodes;
using System.Diagnostics.CodeAnalysis;

namespace LogicalExpressionUnitTest
{
    [ExcludeFromCodeCoverage]
    public class TreeNodeTest
    {
        [Theory]
        [InlineData("A023")]
        [InlineData("1B")]
        [InlineData("")]
        [InlineData("(")]
        public void AtomicFormulaNode_IncorrectName_shouldThrow(string input)
        {
            LogicalExpression.Debug = true;
            TreeNode.DebugLevel = TreeNode.DebugLevels.Eval | TreeNode.DebugLevels.Set | TreeNode.DebugLevels.Clear;

            Assert.Throws<ArgumentException>(() => new AtomicFormulaNode(input));
        }
    }
}