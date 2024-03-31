using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.Minimization;
using LogicalExpressionClassLibrary.Minimization.Strategy;
using System.Diagnostics.CodeAnalysis;

ConsoleLogger.DebugLevel =
    ConsoleLogger.DebugLevels.Warning
    | ConsoleLogger.DebugLevels.Error
    | ConsoleLogger.DebugLevels.Info
    | ConsoleLogger.DebugLevels.Debug
    ;

List<string> TestCases = [
    "(A|B)",
    "( ((¬A)&((B)&(C))) | ((A)&((¬B)&(¬C))) | ((A)&((¬B)&(C))) | ((A)&((B)&(¬C))) | ((A)&((B)&(C))) )",
    "((E&A)→(B~(C|D)))",
    "((A|D)→(B~C))",
    "( ((¬A)&((¬B)&C)) | (((¬A)&B)&C) | ((¬A)&(B&(¬C))) | (A&(B&(¬C))) )",
    "(((A)|(¬B))|((¬C)|(D))) & (((¬A)|(B))|((C)|(D))) & (((¬A)|(B))|((¬C)|(D))) & (((¬A)|(¬B))|((C)|(D))) & (((¬A)|(¬B))|((¬C)|(D)))",
    "(A→(B~C))",
    "((B→A)~C)",
];

LogicalExpression expr = new(TestCases[0]);

Console.WriteLine(expr.ToString());
Console.WriteLine(expr.FCNF);
Console.WriteLine(expr.FDNF);
Console.WriteLine(expr.ToTruthTableString());

var min1FCNF = expr.Minimize(NormalForms.FCNF, new EvaluationStrategy());
Console.WriteLine($"Minimized FCNF with {nameof(EvaluationStrategy)}:".PadRight(45) + min1FCNF.ToString());
var min2FCNF = expr.Minimize(NormalForms.FCNF, new CombinedStrategy());
Console.WriteLine($"Minimized FCNF with {nameof(CombinedStrategy)}:".PadRight(45) + min2FCNF.ToString());
var min3FCNF = expr.Minimize(NormalForms.FCNF, new TableStrategy());
Console.WriteLine($"Minimized FCNF with {nameof(TableStrategy)}:".PadRight(45) + min3FCNF.ToString());

Console.WriteLine();

var min1FDNF = expr.Minimize(NormalForms.FDNF, new EvaluationStrategy());
Console.WriteLine($"Minimized FDNF with {nameof(EvaluationStrategy)}:".PadRight(45) + min1FDNF.ToString());
var min2FDNF = expr.Minimize(NormalForms.FDNF, new CombinedStrategy());
Console.WriteLine($"Minimized FDNF with {nameof(CombinedStrategy)}:".PadRight(45) + min2FDNF.ToString());
var min3FDNF = expr.Minimize(NormalForms.FDNF, new TableStrategy());
Console.WriteLine($"Minimized FDNF with {nameof(TableStrategy)}:".PadRight(45) + min3FDNF.ToString());


[ExcludeFromCodeCoverage]
public partial class Program() { }