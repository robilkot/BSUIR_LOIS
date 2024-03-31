using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.Minimization;
using LogicalExpressionClassLibrary.Minimization.Strategy;
using System.Diagnostics.CodeAnalysis;

ConsoleLogger.DebugLevel =
    ConsoleLogger.DebugLevels.Info
    //| ConsoleLogger.DebugLevels.Warning
     | ConsoleLogger.DebugLevels.Error
    //| ConsoleLogger.DebugLevels.Debug
    ;

List<string> TestCases = [
    "( ((¬A)&((¬B)&C)) | (((¬A)&B)&C) | ((¬A)&(B&(¬C))) | (A&(B&(¬C))) )",
    "( ((¬A)&((B)&(C))) | ((A)&((¬B)&(¬C))) | ((A)&((¬B)&(C))) | ((A)&((B)&(¬C))) | ((A)&((B)&(C))) )",
    "(A→(B~C))",
    "((B→A)~C)",
];

var form = NormalForms.FCNF;

LogicalExpression expr = new(TestCases[0]);
Console.WriteLine(expr.ToString());
//Console.WriteLine(expr.ToTruthTableString());

//var strategy = new EvaluationStrategy();
var strategy = new CombinedStrategy();
var x = expr.Minimize(form, strategy);
Console.WriteLine(x);

//var x = expr.Minimize(form);
//Console.WriteLine(x);


[ExcludeFromCodeCoverage]
public partial class Program() { }