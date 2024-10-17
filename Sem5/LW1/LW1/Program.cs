using LW1.Parsing;

string workingDirectory = Environment.CurrentDirectory;
string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

using var file = File.OpenText($"{projectDirectory}/Input/example1.kb");

string kbString = file.ReadToEnd();

try
{
    var kb = new ParserFacade(kbString).Parse();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}