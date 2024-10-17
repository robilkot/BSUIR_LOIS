using Antlr4.Runtime;
using LW1.Model;

namespace LW1.Parsing
{
    public class ParserFacade
    {
        private readonly FuzzyLogicParser _parser;
        private readonly ExceptionListener _exceptionListener = new();
        private readonly KBVisitor _visitor = new();

        public ParserFacade(string text)
        {
            AntlrInputStream _input_stream = new(text);
            FuzzyLogicLexer _lexer = new(_input_stream);
            CommonTokenStream _stream = new(_lexer);
            _parser = new(_stream);

            _parser.AddErrorListener(_exceptionListener);
        }

        public KB Parse()
            => _parser.kb().Accept(_visitor);
    }
}
