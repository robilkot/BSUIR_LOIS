//
// Лабораторная работа №1 по дисциплине "Логические основы интеллектуальных систем"
// Выполнили студенты группы 221701 БГУИР:
// - Робилко Тимур Маркович
// - Абушкевич Алексей Александрович
// 
// Класс для хранения unit-тестов, относящихся к классу LogicalExpression
// 30.02.2024
//
// Источники:
// - Проектирование программного обеспечения интеллектуальных систем (3 семестр)
// - Библиотека xUnit
//

using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalExpressionTree;
using LogicalExpressionClassLibrary.LogicalParser;
using System.Diagnostics.CodeAnalysis;

namespace LogicalExpressionUnitTest
{
    //"(A1&(A2|(¬(A3|(A4|A5)))))"
    //"((A|B)&(¬C))"
    //"((¬A)|(¬(B&C)))"
    //"(A&(B&C))",
    //"(A&(B|C))",
    //"(A→(B→C))",
    //"(B|C)",
    //"((A1|(A2&A3))|B1)",
    //"((A1|F)|T)",
    //"((P|(Q&R))|S)",
    //"((A1|(&A3))|B1)",

    // pz 2
    // "(((!P)>P)>(R|Q))"
    // "((((P>R)&(Q>S))&((!P)|(!S)))>((!P)|(!Q)))"

    [ExcludeFromCodeCoverage]
    public class LogicalExpressionUnitTest
    {
        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("((A\/(B/\C))\/1)")]
        [InlineData("((A\/0)\/1)")]
        [InlineData("(!(!A))")]
        [InlineData("(!(!(!(!A))))")]
        [InlineData("((A\/(B/\C))\/D)")]
        public void RecursiveParsing_shouldEqual(string input)
        {
            LogicalExpression expr = new(input, new RecursiveLogicalParser());

            Assert.Equal(input, expr.ToString());
        }

        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("((A\/(B/\C))\/D)")]
        [InlineData("((A\/0)\/1)")]
        [InlineData("(!(!A))")]
        [InlineData("(!(!(!(!A))))")]
        [InlineData("((A\/(B/\C))\/D)")]
        public void GetTruthTable_shouldNotThrow(string input)
        {
            LogicalExpression expr = new(input);

            var exception = Record.Exception(() => _ = expr.TruthTable);
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("((A\/B)\/C)", true, true, true, true)]
        [InlineData("((A\/B)\/C)", false, false, false, false)]
        [InlineData("((A/\C)\/C)", false, false, true, true)]
        [InlineData("((A/\B)\/C)", true, true, false, true)]
        [InlineData("((A/\A)/\C)", true, false, false, false)]
        [InlineData("((A/\B)/\C)", true, true, true, true)]
        [InlineData("((A->B)/\C)", true, false, true, false)]
        [InlineData("((A->B)/\C)", false, false, true, true)]
        [InlineData("((A/\B)~C)", true, true, true, true)]
        [InlineData("((A/\B)~C)", false, false, false, true)]
        [InlineData("((!B)/\A)\/C)", false, true, false, true)]
        [InlineData("((!A)/\B)\/C)", true, true, false, false)]
        public void Evaluation_shouldEqualExprected(string input, bool A, bool B, bool C, bool result)
        {
            LogicalExpression expr = new(input);
            expr.SetVariable("A", A);
            expr.SetVariable("B", B);
            expr.SetVariable("C", C);

            var actualResult = expr.Evaluation;

            Assert.Equal(result, actualResult);
        }

        [Theory]
        [InlineData("(1)")]
        [InlineData("(A)")]
        [InlineData("(A1B)")]
        [InlineData("(A=B1)")]
        [InlineData("(AB&)")]
        [InlineData("(!)")]
        [InlineData("(A/\(\/C))")]
        [InlineData("(1_0_\(\/?))")]
        [InlineData("(A/\B/\C))))))))))))))))")]
        [InlineData("A()C")]
        [InlineData("1?__)")]
        [InlineData("A->B->C->D->E->F->G->H->I")]
        [InlineData("(A->B->C->D->E->F->(G->H)->I)")]
        [InlineData("A/\B/\C/\D/\E/\F/\G/\H/\I/\И/\К\/О/\П")]
        [InlineData("(0)")]
        [InlineData("(G)")]
        [InlineData("(AG)")]
        [InlineData("(A->B1234567)")]
        [InlineData("(AB/\&&C)")]

        public void Parsing_IncorrectNotation_shouldThrow(string input)
        {
            Assert.Throws<ArgumentException>(() => new LogicalExpression(input));
        }

        [Fact]
        public void SettingVariable_VariableChangesValue_ShouldResetEvaluation()
        {
            LogicalExpression expr = new("((A/\B)~C)");
            expr.SetVariable("A", true);
            expr.SetVariable("B", false);
            expr.SetVariable("C", false);
            var oldResult = expr.Evaluation;

            expr.SetVariable("B", true);

            Assert.NotEqual(oldResult, expr.Evaluation);
        }
    }
}