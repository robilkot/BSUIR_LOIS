using LogicalExpressionClassLibrary;
using LogicalExpressionClassLibrary.LogicalExpressionTree;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


//LogicalExpression.Debug = true;
//TreeNode.DebugLevel = TreeNode.DebugLevels.Eval | TreeNode.DebugLevels.Set | TreeNode.DebugLevels.Clear;

List<string> TestCases = [
    "(A&B)",
    "(A)",
    "(A&(B|C))",
    "(B&C)",
    $"(A{(char)LogicalSymbols.Implication}(B~C))",
    $"(({(char)LogicalSymbols.Negation}B)&C)",
    $"(({(char)LogicalSymbols.Negation}A)|C)",
    $"((B{(char)LogicalSymbols.Implication}A)~C)",
];

void InputExpression(out LogicalExpression expr)
{
    while (true)
    {
        try
        {
            expr = new(Console.ReadLine()!);
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Please, re-input expression");
        }
    }
}

void CheckImplication()
{
    Console.WriteLine("Input first formula:");
    InputExpression(out LogicalExpression expr1);

    Console.WriteLine("Input second formula:");
    InputExpression(out LogicalExpression expr2);

    Console.WriteLine(expr1.ToTruthTableString());
    Console.WriteLine(expr2.ToTruthTableString());
    Console.WriteLine($"{expr2} " + (expr2.ImpliesFrom(expr1) ? "implies" : "doesn't imply") + $" from {expr1}\n");
}

void CheckKnowledge()
{
    var random = new Random();

    int index = random.Next(TestCases.Count);
    LogicalExpression expr1 = new(TestCases[index]);

    index = random.Next(TestCases.Count);
    LogicalExpression expr2 = new(TestCases[index]);

    bool correctAnswer = expr1.ImpliesFrom(expr2);

    Console.WriteLine($"First expression: {expr1}");
    Console.WriteLine($"Second expression: {expr2}");
    Console.WriteLine("Does second expression imply from first one? Y/N");
    char userKeyInput = Console.ReadKey().KeyChar;
    bool userAnswer = userKeyInput == 'Y' || userKeyInput == 'y';

    if (correctAnswer == userAnswer) Console.WriteLine("\nCorrect!");
    else Console.WriteLine("\nIncorrect!");
}

while (true)
{
    Console.WriteLine("To check implication, press 1.\nTo check your knowledge, press 2.");

    switch (Console.ReadLine())
    {
        case "1":
            {
                CheckImplication();
                break;
            }
        case "2":
            {
                CheckKnowledge();
                break;
            }
    }
}


[ExcludeFromCodeCoverage]
public partial class Program() { }