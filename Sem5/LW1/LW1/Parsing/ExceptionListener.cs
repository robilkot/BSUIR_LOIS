using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace LW1.Parsing
{
    public class ExceptionListener : BaseErrorListener
    {
        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            Console.WriteLine(msg);
        }
    }
}
