using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalExpressionTree;
using System.Diagnostics.CodeAnalysis;

List<string> tests =
[
    //"(A1&(A2|(¬(A4|A5))))"
    "((A|B)&(¬C))"
    //"(A&(B&C))",
    //"(A&B)",
    //"(A→(B→C))",
    //"(B→C)",
    //"((A1|(A2&A3))|B1)",
    //"((A1|F)|T)",
    //"((P|(Q&R))|S)",
    //"((A1|(&A3))|B1)",
];

//LogicalExpression.Debug = true;
//TreeNode.DebugLevel = TreeNode.DebugLevels.Eval | TreeNode.DebugLevels.Set | TreeNode.DebugLevels.Clear;

foreach (var test in tests)
{
    try
    {
        LogicalExpression expr = new(test);
        Console.WriteLine();
        Console.WriteLine(expr.ToTruthTableString());
        Console.WriteLine();
        Console.WriteLine(expr.NumberForm);

        //Console.WriteLine();
        //Console.WriteLine(expr2.ToTruthTableString());
        //Console.WriteLine();
        //Console.WriteLine(expr2.NumberForm);

        LogicalExpression expr2 = expr.ToFDNF();
        Console.WriteLine(expr2.ToString());
        Console.WriteLine(expr2.ToFDNFNumericString());

        LogicalExpression expr3 = expr.ToFCNF();
        Console.WriteLine(expr3.ToString());
        Console.WriteLine(expr3.ToFCNFNumericString());


        //LogicalExpression expr3 = expr.ToFCNF();
        //Console.WriteLine();
        //Console.WriteLine(expr3.ToTruthTableString());
        //Console.WriteLine();
        //Console.WriteLine(expr3.NumberForm);

        //Console.WriteLine(expr3.ToString());

        //expr.SetVariable("A", true);
        //expr.SetVariable("B", true);
        //Console.WriteLine(expr.Evaluation);
        //Console.WriteLine(expr2.Evaluation);
        //Console.WriteLine(expr3.Evaluation);

        //expr2.SetVariable("A", true);
        //expr2.SetVariable("B", true);
        //Console.WriteLine(expr.Evaluation);
        //Console.WriteLine(expr2.Evaluation);
        //Console.WriteLine(expr3.Evaluation);

        //expr.SetVariable("C", false);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
}

[ExcludeFromCodeCoverage]
public partial class Program() { }