using LW1.FuzzyLogic;
using LW1.Parsing;

string workingDirectory = Environment.CurrentDirectory;
string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

using var file = File.OpenText($"{projectDirectory}/Input/example1.kb");

string kbString = file.ReadToEnd();

try
{
    var kb = new ParserFacade(kbString).Parse();

    var res = Inference.Run(kb);

    foreach (var item in res)
    {
        Console.WriteLine(item);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}