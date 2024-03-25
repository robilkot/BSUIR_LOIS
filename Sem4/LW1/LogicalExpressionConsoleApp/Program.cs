using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.Minimization;
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

var x = expr.Minimize(form);
Console.WriteLine(x);

[ExcludeFromCodeCoverage]
public partial class Program() { }