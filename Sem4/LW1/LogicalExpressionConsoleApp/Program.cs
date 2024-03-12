using LogicalExpressionClassLibrary;
using System.Diagnostics.CodeAnalysis;

List<string> tests =
[
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
];

//LogicalExpression.Debug = true;
//TreeNode.DebugLevel = TreeNode.DebugLevels.Eval | TreeNode.DebugLevels.Set | TreeNode.DebugLevels.Clear;

try
{
    LogicalExpression expr = new("(A→B)");
    Console.WriteLine(expr.ToTruthTableString());
    //Console.WriteLine($"Number form: {expr.NumberForm}");

    LogicalExpression expr2 = new("(A→B)");
    Console.WriteLine(expr2.ToTruthTableString());

    Console.WriteLine($"{expr2} " + (expr2.ImpliesFrom(expr) ? "implies" : "doesn't imply") + $" from {expr}");

    //LogicalExpression expr2 = expr.FDNF;
    //Console.WriteLine($"FDNF: {expr2}");
    //Console.WriteLine(expr2.ToFDNFNumericString());

    //LogicalExpression expr3 = expr.FCNF;
    //Console.WriteLine($"FCNF: {expr3}");
    //Console.WriteLine(expr3.ToFCNFNumericString());

    //expr.SetVariable("A", true);
    //expr.SetVariable("B", true);
    //Console.WriteLine(expr.Evaluation);
    //Console.WriteLine(expr2.Evaluation);
    //Console.WriteLine(expr3.Evaluation);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

[ExcludeFromCodeCoverage]
public partial class Program() { }