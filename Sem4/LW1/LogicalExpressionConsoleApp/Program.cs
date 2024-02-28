using LogicalExpressionClassLibrary;

List<string> tests =
[
    "((A1|(A2&A3))|B1)",
    "B",
    "((A1|F)|T)",
    "(¬(¬A123))",
    "((P|(Q&R))|S)",
    "((A1|(&A3))|B1)",
];

foreach (var test in tests)
{
    try
    {
        LogicalExpression expr = new(test);
        //Console.WriteLine(test);
        Console.WriteLine(expr.ToString());
        Console.WriteLine(expr.TruthTable.ToTableString());
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }

    //expr.SetVariable("A1", false);
    //expr.SetVariable("A2", false);
    //expr.SetVariable("A3", true);
    //expr.SetVariable("B1", false);

    //Console.WriteLine(expr.Evaluate());
}
