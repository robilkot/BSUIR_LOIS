using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.Minimization;
using LogicalExpressionClassLibrary.Minimization.Strategy;
using System.Diagnostics.CodeAnalysis;

ConsoleLogger.DebugLevel =
    ConsoleLogger.DebugLevels.Warning
    | ConsoleLogger.DebugLevels.Error
    | ConsoleLogger.DebugLevels.Info
    //| ConsoleLogger.DebugLevels.Debug
    ;

List<string> TestCases = [
    "( ((¬A)&((¬B)&C)) | (((¬A)&B)&C) | ((¬A)&(B&(¬C))) | (A&(B&(¬C))) )",
    "( ((¬A)&((B)&(C))) | ((A)&((¬B)&(¬C))) | ((A)&((¬B)&(C))) | ((A)&((B)&(¬C))) | ((A)&((B)&(C))) )",
    "(A→(B~C))",
    "((B→A)~C)",
];

LogicalExpression expr = new(TestCases[0]);
Console.WriteLine(expr.ToString());
//Console.WriteLine(expr.ToTruthTableString());

//var min1FCNF = expr.Minimize(NormalForms.FCNF, new EvaluationStrategy());
//Console.WriteLine(min1FCNF);
//var min2FCNF = expr.Minimize(NormalForms.FCNF, new CombinedStrategy());
//Console.WriteLine(min2FCNF);

var min1FDNF = expr.Minimize(NormalForms.FDNF, new EvaluationStrategy());
Console.WriteLine(min1FDNF);
var min2FDNF = expr.Minimize(NormalForms.FDNF, new CombinedStrategy());
Console.WriteLine(min2FDNF);

//var x = expr.Minimize(form);
//Console.WriteLine(x);


[ExcludeFromCodeCoverage]
public partial class Program() { }