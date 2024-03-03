using LogicalExpressionClassLibrary;

List<string> tests =
[
    //"(A1&(A2|(¬(A4|A5))))"
    //"((A|B)&(¬C))"
    //"(A&(B&C))",
    "(A15&B5)",
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
        Console.WriteLine(expr.IndexForm);

        LogicalExpression expr2 = expr.ToFDNF();
        Console.WriteLine();
        Console.WriteLine(expr2.ToTruthTableString());
        Console.WriteLine();
        Console.WriteLine(expr2.IndexForm);

        Console.WriteLine(expr2.ToString());

        LogicalExpression expr3 = expr.ToFCNF();
        Console.WriteLine();
        Console.WriteLine(expr3.ToTruthTableString());
        Console.WriteLine();
        Console.WriteLine(expr3.IndexForm);

        Console.WriteLine(expr3.ToString());

        //expr.SetVariable("A", true);
        //expr.SetVariable("B", false);
        //expr.SetVariable("C", false);
        //Console.WriteLine(expr.Evaluation);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
}