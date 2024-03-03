using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalExpressionTree;

List<string> tests =
[
    //"(A1&(A2|(¬(A4|A5))))"
    "((A|B)&(¬C))"
    //"(A&(B&C))",
    //"(A15&B5)",
    //"(A→(B→C))",
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
        Console.WriteLine(expr.ToString());
        string tableString = expr.ToTruthTableString();
        Console.WriteLine();
        Console.WriteLine(tableString);
        Console.WriteLine();
        Console.WriteLine(expr.IndexForm);

        //expr.SetVariable("A", true);
        //expr.SetVariable("B", false);
        //expr.SetVariable("C", false);
        Console.WriteLine(expr.Evaluate());
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
}